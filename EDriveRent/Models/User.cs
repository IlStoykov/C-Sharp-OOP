using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Threading;


namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private double rating;
        private string drivingLicenseNumber;

        public User(string firstName, string lastName, string drivingLicenseNumber) {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenseNumber;
            Rating = 0;
            IsBlocked = false;
        }
        public string FirstName { 
            get => firstName;
            private set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentNullException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }
        public string LastName { 
            get => lastName;
            private set {
                if (string.IsNullOrWhiteSpace(value)) { 
                    throw new AbandonedMutexException(ExceptionMessages.LastNameNull);
                }
                lastName = value;
            }
        }
        public double Rating {
            get => rating;
            private set {
                if (value > 10) { rating = 10; }
                else if(value < 0) { rating = 0; }
                else { rating = value; }
            }
        }
        public string DrivingLicenseNumber { 
            get => drivingLicenseNumber;
            private set {
                if (string.IsNullOrWhiteSpace(value)){
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }
                drivingLicenseNumber = value;
            }
        }
        public bool IsBlocked { 
            get; private set;
        }
        public void DecreaseRating()
        {
            rating -= 2.0;
            if (rating < 0.0) { 
                rating = 0.0;
                IsBlocked = true;
            }
        }
        public void IncreaseRating()
        {
            rating += .5;
            if (rating > 10.0) {
                rating = 10.0;
            }
        }
        public override string ToString() => $"{FirstName} {LastName} Driving license: {DrivingLicenseNumber} Rating: {Rating:F2}";
    }
}
