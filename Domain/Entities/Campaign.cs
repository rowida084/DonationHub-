using DonationHub.Domain.Entities;
using DonationHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    public class Campaign
    {
        public Guid Id { get; private set; }
        public string TitleAr { get; private set; } = string.Empty;
        public string TitleEn { get; private set; } = string.Empty;
        public string DescriptionAr { get; private set; } = string.Empty;
        public string DescriptionEn { get; private set; } = string.Empty;
        public decimal GoalAmount { get; private set; }
        public decimal RaisedAmount { get; private set; }
        public DateTime EndDate { get; private set; }
        public Guid CategoryId { get; private set; }
        public string? BannerFileName { get; private set; }//Optional
        public CampaignStatus Status { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public byte[]? RowVersion { get; private set; }

        private Campaign()
        {

        }

        public void EnsureCanReceiveDonation(DateTime curretDate)
        {
            if(Status!=CampaignStatus.Active)
            {
                throw new InvalidOperationException("This Compain is not active now");
            }

            if(EndDate <= curretDate)
            {
                throw new InvalidOperationException("Campaign has expired.");
            }
        }
        public static Campaign Create(
        string titleAr,
        string titleEn,
        string descriptionAr,
        string descriptionEn,
        decimal goalAmount,
        DateTime endDate,
        Guid categoryId,
        Guid createdByUserId,
        DateTime currentTime,
        string? bannerFileName = null)
        {
         
            if (goalAmount <= 0 )
                throw new ArgumentException("Goal amount must be greater than zero.");

            if (endDate <= currentTime)
                throw new ArgumentException("End date must be in the future.");

            return new Campaign
            {
                Id = Guid.NewGuid(),
                TitleAr = titleAr,
                TitleEn = titleEn,
                DescriptionAr = descriptionAr,
                DescriptionEn = descriptionEn,
                GoalAmount = goalAmount,
                RaisedAmount = 0,
                EndDate = endDate,
                CategoryId = categoryId,
                CreatedByUserId = createdByUserId,
                BannerFileName = bannerFileName,
                Status = CampaignStatus.Active,
                CreatedAt = currentTime,
                UpdatedAt = currentTime,
                IsDeleted = false
            };
        }


        private void EnsureCanUpdateEndDate(DateTime newEndDate, DateTime currentTime)
        {
            if (newEndDate <= currentTime)
                throw new InvalidCastException("The new end date must be in the future");
        }
        public void UpdateEndDate(DateTime newEndDate, DateTime currentTime)
        {
            EnsureCanUpdateEndDate(newEndDate, currentTime);
            EndDate = newEndDate;
            UpdatedAt = currentTime;
        }

        private bool EnsureCampaignComplete()
        {
            if (RaisedAmount >= GoalAmount)
            {
                Status = CampaignStatus.Completed;
                return true;
            }

            return false;
        }
        public void AddDonation(Donation donation)
        {
            if (donation == null)
                throw new ArgumentNullException("Can't Accept Nullable Donation.");

            if (donation.Status != DonationStatus.Succeeded)
                throw new InvalidOperationException
                    ("Only succeeded donations can update raised amount.");

            if (Status != CampaignStatus.Active)
                throw new InvalidOperationException("Only active campaigns can receive successful donations.");

            if (Id != donation.CampaignId)
                throw new ArgumentException
                    ("This donation does not belong to this campaign.",nameof(donation));


            RaisedAmount += donation.Amount;
            EnsureCampaignComplete();
        }
        public void UpdateGoalAmount(decimal newGoalAmount)
        {
            if (newGoalAmount <= 0)
                throw new ArgumentException("Goal amount must be greater than zero.");

            if (GoalAmount< RaisedAmount)
            {
                throw new InvalidOperationException
                    ("Goal amount cannot be lower than raised amount.");
            }

            GoalAmount = newGoalAmount;
        }
        public void ChangeCampaignStatus(CampaignStatus newStatus)
        {
            if (Status != CampaignStatus.Active)
                throw new InvalidOperationException
                    ("Campaign status cannot be changed from a terminal state.");

            if (newStatus != CampaignStatus.Completed &&
                  newStatus != CampaignStatus.Expired)
                throw new ArgumentException
                   ("Invalid campaign status transition.");


            Status = newStatus;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
    }
