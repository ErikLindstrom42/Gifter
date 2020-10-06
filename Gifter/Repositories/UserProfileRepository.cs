using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Gifter.Models;
using Gifter.Utils;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, Name, Bio, Email, DateCreated, ImageUrl
                FROM UserProfile 
                       ORDER BY Name";

                    var reader = cmd.ExecuteReader();

                    var userProfiles = new List<UserProfile>();
                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {

                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl")
                            
                        });
                    }

                    reader.Close();

                    return userProfiles;
                }
            }
        }

        //public List<Post> GetAllWithComments()
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated,
        //               p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId,
        //               up.Name, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated,
        //               up.ImageUrl AS UserProfileImageUrl,
        //               c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
        //        FROM Post p
        //               LEFT JOIN UserProfile up ON p.UserProfileId = up.id
        //               LEFT JOIN Comment c on c.PostId = p.id
        //        ORDER BY p.DateCreated";

        //            var reader = cmd.ExecuteReader();

        //            var userProfiles = new List<Post>();
        //            while (reader.Read())
        //            {
        //                var userProfileId = DbUtils.GetInt(reader, "PostId");

        //                var existingPost = userProfiles.FirstOrDefault(p => p.Id == userProfileId);
        //                if (existingPost == null)
        //                {
        //                    existingPost = new Post()
        //                    {
        //                        Id = userProfileId,
        //                        Title = DbUtils.GetString(reader, "Title"),
        //                        Caption = DbUtils.GetString(reader, "Caption"),
        //                        DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
        //                        ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
        //                        UserProfileId = DbUtils.GetInt(reader, "PostUserProfileId"),
        //                        UserProfile = new UserProfile()
        //                        {
        //                            Id = DbUtils.GetInt(reader, "PostUserProfileId"),
        //                            Name = DbUtils.GetString(reader, "Name"),
        //                            Email = DbUtils.GetString(reader, "Email"),
        //                            DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
        //                            ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
        //                        },
        //                        Comments = new List<Comment>()
        //                    };

        //                    userProfiles.Add(existingPost);
        //                }

        //                if (DbUtils.IsNotDbNull(reader, "CommentId"))
        //                {
        //                    existingPost.Comments.Add(new Comment()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "CommentId"),
        //                        Message = DbUtils.GetString(reader, "Message"),
        //                        PostId = userProfileId,
        //                        UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
        //                    });
        //                }
        //            }

        //            reader.Close();

        //            return userProfiles;
        //        }
        //    }
        //}
        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Id, Name, Bio, Email, DateCreated, ImageUrl
                                        FROM UserProfile 
                                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();

                    UserProfile userProfile = null;
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl")

                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }



        //public Post GetPostByIdWithComments(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                  SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated, 
        //                    p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId, 
        //                    c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
        //                    FROM Post p
        //                    LEFT JOIN Comment c ON c.PostId = p.Id
        //                    WHERE p.Id = @Id";

        //            DbUtils.AddParameter(cmd, "@Id", id);




        //            var reader = cmd.ExecuteReader();

        //            Post userProfile = null;
        //            List<Comment> comments = new List<Comment>();
        //            while (reader.Read())
        //            {

        //                userProfile = new Post()
        //                {
        //                    Id = id,
        //                    Title = DbUtils.GetString(reader, "Title"),
        //                    Caption = DbUtils.GetString(reader, "Caption"),
        //                    DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
        //                    ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
        //                    UserProfileId = DbUtils.GetInt(reader, "PostUserProfileId"),
        //                    Comments = new List<Comment>()
        //                };
        //                if (DbUtils.IsNotDbNull(reader, "CommentId"))
        //                {
        //                    comments.Add(new Comment()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "CommentId"),
        //                        Message = DbUtils.GetString(reader, "Message"),
        //                        PostId = id,
        //                        UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
        //                    });
        //                }
        //            }
        //            userProfile.Comments = comments;
        //            reader.Close();

        //            return userProfile;
        //        }
        //    }
        //}
        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Post (Name, Email, ImageUrl, Bio, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Email, @ImageUrl, @Bio, @DateCreated)";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Bio", userProfile.Bio);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Post
                           SET Name = @Name,
                               Email = @Email,
                               DateCreated = @DateCreated,
                               ImageUrl = @ImageUrl,
                               Bio = @Bio
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Bio", userProfile.Bio);
                    DbUtils.AddParameter(cmd, "@Id", userProfile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //public List<Post> Search(string criterion, bool sortDescending)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            var sql =
        //                @"SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated, 
        //                p.ImageUrl AS PostImageUrl, p.UserProfileId,

        //                up.Name, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated, 
        //                up.ImageUrl AS UserProfileImageUrl
        //            FROM Post p 
        //                LEFT JOIN UserProfile up ON p.UserProfileId = up.id
        //            WHERE p.Title LIKE @Criterion OR p.Caption LIKE @Criterion";

        //            if (sortDescending)
        //            {
        //                sql += " ORDER BY p.DateCreated DESC";
        //            }
        //            else
        //            {
        //                sql += " ORDER BY p.DateCreated";
        //            }

        //            cmd.CommandText = sql;
        //            DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");
        //            var reader = cmd.ExecuteReader();

        //            var userProfiles = new List<Post>();
        //            while (reader.Read())
        //            {
        //                userProfiles.Add(new Post()
        //                {
        //                    Id = DbUtils.GetInt(reader, "PostId"),
        //                    Title = DbUtils.GetString(reader, "Title"),
        //                    Caption = DbUtils.GetString(reader, "Caption"),
        //                    DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
        //                    ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
        //                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
        //                    UserProfile = new UserProfile()
        //                    {
        //                        Id = DbUtils.GetInt(reader, "UserProfileId"),
        //                        Name = DbUtils.GetString(reader, "Name"),
        //                        Email = DbUtils.GetString(reader, "Email"),
        //                        DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
        //                        ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
        //                    },
        //                });
        //            }

        //            reader.Close();

        //            return userProfiles;
        //        }
        //    }
        //}
    }
}