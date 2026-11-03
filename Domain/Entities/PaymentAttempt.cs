using DonationHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PaymentAttempt
    {
        public Guid Id { get; private set; }

        public Guid DonationId { get; private set; }

        public string? StripeCheckoutSessionId { get; private set; } 

        public string? StripePaymentIntentId { get; private set; }

        public decimal Amount { get; private set; }

        public PaymentAttemptStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public string? FailureCode { get; private set; }

        private PaymentAttempt() { }

        public static PaymentAttempt Create
            (Guid donationId,string stripeCheckoutSessionId,decimal amount,DateTime createdAt)
        {
            if (donationId == Guid.Empty)
                throw new ArgumentException("DonationId is required.");

            if (string.IsNullOrWhiteSpace(stripeCheckoutSessionId))
                throw new ArgumentException("Checkout Session Id is required.");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            return new PaymentAttempt
            {
                Id = Guid.NewGuid(),
                DonationId = donationId,
                StripeCheckoutSessionId = stripeCheckoutSessionId,
                Amount = amount,
                Status = PaymentAttemptStatus.Created,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
        }

        public void ChangePaymentStatus(PaymentAttemptStatus newStatus)
        {
            if (Status != PaymentAttemptStatus.Created)
                throw new InvalidOperationException
                    ("Payment status cannot be changed from a terminal state.");

            if (newStatus == PaymentAttemptStatus.Created)
                throw new ArgumentException
                        ("New status must be a final payment status");
            Status = newStatus;
        }
        
    }
}
