using System;
using Zomlypse.Enums;

namespace Zomlypse
{
    public struct CharacterInfo
    {
        public string Forename { get; }
        public string Surname { get; }
        public string Fullname
        {
            get
            {
                if (IsPlayer)
                {
                    return Forename;
                }
                if (string.IsNullOrEmpty(Surname))
                {
                    return Forename;
                }

                return $"{Forename} {Surname}";
            }
        }
        public int Age { get; private set; }
        public Gender Gender { get; set; }
        public bool IsPlayer { get; }
        public Guid Id { get; }

        public CharacterInfo(string name, int age, Gender gender, bool isPlayer = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} cannot be empty or null");
            }

            IsPlayer = isPlayer;
            if (IsPlayer)
            {
                Forename = name;
                Surname = name;
            }
            else
            {
                string[] parts = name.Split('_');
                if (parts.Length != 2)
                {
                    throw new ArgumentException($"{nameof(name)} is invalid naming format");
                }

                Forename = parts[0];
                Surname = parts[1];
            }
            
            Age = age;
            Gender = gender;
            Id = Guid.NewGuid();
        }

        public static CharacterInfo Empty()
        {
            return new CharacterInfo("Empty Empty", 0, Gender.Male);
        }

        public static CharacterInfo EmptyPlayer()
        {
            return new CharacterInfo("Player", 0, Gender.Male, true);
        }
    }
}
