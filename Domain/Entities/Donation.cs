using System;
using System.Timers;
using DonationHub.Domain.Enums;

namespace DonationHub.Domain.Entities
{
    public class Donation
    {
        public Guid Id { get; private set; }
        public Guid CampaignId { get; private set; }
        public Guid DonorUserId { get; private set; }
        public decimal Amount { get; private set; }
        public DonationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public string? FailureReason { get; private set; }
        public int? ReceiptNumber { get; private set; }
        public string? ReceiptFileName { get; private set; }

        private Donation() { }

        
        public static Donation Create(
            Guid campaignId,
            Guid donorUserId,
            decimal amount,
            DateTime createdAt,
            DateTime? completedAt = null,
            string? failureReason = null,
            int? receiptNumber = null,
            string? receiptFileName = null)
        {
            if (campaignId == Guid.Empty)
                throw new ArgumentException("CampaignId is required.");

            if (donorUserId == Guid.Empty)
                throw new ArgumentException("DonorUserId is required.");

            if (amount < 1 && amount > 10000)
                throw new ArgumentException("Invalid amount (The amount must be >= 1.00 and <=10000)");

            return new Donation
            {
                Id = Guid.NewGuid(),
                CampaignId = campaignId,
                DonorUserId = donorUserId,
                Amount = amount,
                Status = DonationStatus.Pending,
                CreatedAt = createdAt,
                CompletedAt = null,
                FailureReason = null,
                ReceiptNumber = null,
                ReceiptFileName = null
            };
        }

      public void MarkAsSucceeded(DateTime currentDate)
        {
            if (Status != DonationStatus.Pending)
                throw new InvalidOperationException
                    ("Only pending donations can be marked as succeeded.");
            Status = DonationStatus.Succeeded;
            CompletedAt = currentDate;
        }

        public void MarkAsFailed(string failureReason, DateTime currentTime)
        {
            if (Status != DonationStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending donations can be marked as failed.");

            if (string.IsNullOrWhiteSpace(failureReason))
                throw new ArgumentException("Failure reason is required.");

            Status = DonationStatus.Failed;
            CompletedAt = currentTime;
            FailureReason = failureReason;
        }
    }
}