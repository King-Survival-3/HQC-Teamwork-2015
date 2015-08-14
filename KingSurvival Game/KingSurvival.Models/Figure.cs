namespace KingSurvival.Models
{
    using System;

    public abstract class Figure
    {
        private string name;

        public Figure(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (Validator.IsAlphabeticString(value))
                {
                    throw new ArgumentException(
                        "Invalid value passed at figure setter");
                }

                this.name = value;
            }
        }
    }
}