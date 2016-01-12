using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Profile;
using DalToWeb;
using DalToWeb.Repositories;

namespace CustomAuth.Providers
{
    public class CustomProfileProvider : ProfileProvider
    {
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            // коллекция, которая возвращает значения свойств профиля
            var result = new SettingsPropertyValueCollection();

            if (collection == null || collection.Count < 1 || context == null)
            {
                return result;
            }

            // получаем из контекста имя пользователя - логин в системе
            var username = (string)context["UserName"];
            if (String.IsNullOrEmpty(username)) return result;

            var db = new DatabaseContext();
            // получаем id пользователя из таблицы Users по логину
            var firstOrDefault = db.Users.FirstOrDefault(u => u.Email.Equals(username));
            if (firstOrDefault != null)
            {
                int userId = firstOrDefault.Id;
                // по этому id извлекаем профиль из таблицы профилей
                Profile profile = db.Profiles.FirstOrDefault(u => u.UserId == userId);
                if (profile != null)
                {
                    foreach (SettingsProperty prop in collection)
                    {
                        var spv = new SettingsPropertyValue(prop)
                        {
                            PropertyValue = profile.GetType().GetProperty(prop.Name).GetValue(profile, null)
                        };
                        result.Add(spv);
                    }
                }
                else
                {
                    foreach (SettingsProperty prop in collection)
                    {
                        var svp = new SettingsPropertyValue(prop) {PropertyValue = null};
                        result.Add(svp);
                    }
                }
            }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            // получаем логин пользователя
            var username = (string)context["UserName"];

            if (string.IsNullOrEmpty(username) || collection.Count < 1)
                return;

            var db = new DatabaseContext();
            // получаем id пользователя из таблицы Users по логину
            var firstOrDefault = db.Users.FirstOrDefault(u => u.Email.Equals(username));
            if (firstOrDefault != null)
            {
                int userId = firstOrDefault.Id;
                // по этому id извлекаем профиль из таблицы профилей
                Profile profile = db.Profiles.FirstOrDefault(u => u.UserId == userId);
                // если такой профиль уже есть изменяем его
                if (profile != null)
                {
                    foreach (SettingsPropertyValue val in collection)
                    {
                        profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                    }
                    profile.LastUpdateDate = DateTime.Now;
                    db.Entry(profile).State = EntityState.Modified;
                }
                else
                {
                    // если нет, то создаем новый профиль и добавляем его
                    profile = new Profile();
                    foreach (SettingsPropertyValue val in collection)
                    {
                        profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                    }
                    profile.LastUpdateDate = DateTime.Now;
                    profile.UserId = userId;
                    db.Profiles.Add(profile);
                }
            }
            db.SaveChanges();
        }

        public override string ApplicationName { get; set; }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch,
            int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption,
            string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
    }
}