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
        public Gender Gender { get; }
        public bool IsPlayer { get; }

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
                string[] parts = name.Split(' ');
                if (parts.Length != 2)
                {
                    throw new ArgumentException($"{nameof(name)} is invalid naming format");
                }

                Forename = parts[0];
                Surname = parts[1];
            }
            
            Age = age;
            Gender = gender;
        }
    }
}
