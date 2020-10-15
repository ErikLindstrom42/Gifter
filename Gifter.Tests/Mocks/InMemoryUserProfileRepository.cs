using System;
using System.Collections.Generic;
using System.Linq;
using Gifter.Models;
using Gifter.Repositories;

namespace Gifter.Tests.Mocks
{
    class InMemoryUserProfileRepository : IUserProfileRepository
    {
        private readonly List<UserProfile> _data;

        public List<UserProfile> InternalData
        {
            get
            {
                return _data;
            }

        }

        public InMemoryUserProfileRepository(List<UserProfile> startingData)
        {
            _data = startingData;
        }
        public void Add(UserProfile userProfile)
        {
            var lastUserProfile = _data.Last();
            userProfile.Id = lastUserProfile.Id + 1;
            _data.Add(userProfile);
        }

        public void Delete(int id)
        {
            var userProfileToDelete = _data.FirstOrDefault(up => up.Id == id);
            if(userProfileToDelete == null)
            {
                return;
            }
            _data.Remove(userProfileToDelete);
        }

        public List<UserProfile> GetAll()
        {
            return _data;
        }

        public UserProfile GetById(int id)
        {
            return _data.FirstOrDefault(up => up.Id == id);
        }

        public void Update(UserProfile userProfile)
        {
            var currentUserProfile = _data.FirstOrDefault(up => up.Id == userProfile.Id);
            if (currentUserProfile == null)
            {
                return;
            }

            currentUserProfile.Name = userProfile.Name;
            currentUserProfile.ImageUrl = userProfile.ImageUrl;
            currentUserProfile.FirebaseUserId = userProfile.FirebaseUserId;
            currentUserProfile.Email = userProfile.Email;
            currentUserProfile.Bio = userProfile.Bio;
        }
        //BELOW IS NOT A REAL METHOD
        public UserProfile GetByFirebaseUserId (string firebaseUserId)
        {
            return _data.FirstOrDefault();
        }



    }
}
