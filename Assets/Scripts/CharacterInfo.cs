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
                if (string.IsNullOrEmpty(Surname))
                {
                    return Forename;
                }

                return $"{Forename} {Surname}";
            }
        }
        public int Age { get; private set; }
        public Gender Gender { get; }

        public CharacterInfo(string name, int age, Gender gender)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} cannot be empty or null");
            }

            string[] parts = name.Split(' ');
            Forename = parts[0];
            Surname = parts.Length > 1
                ? parts[1]
                : default;
            
            Age = age;
            Gender = gender;
        }
    }
}
