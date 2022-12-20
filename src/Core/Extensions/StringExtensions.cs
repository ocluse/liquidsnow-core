using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Ocluse.LiquidSnow.Core.Extensions
{
    /// <summary>
    /// Extensions for the System.Collections.Generic namespace.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to the block format, where the first letter is capitalized and the rest are converted to small letters.
        /// </summary>
        /// <returns>A string that has been converted to block format</returns>
        public static string ToBlock(this string? s)
        {
            // Check for empty string.  
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.  
            return char.ToUpper(s[0]) + s[1..].ToLower();
        }

        /// <summary>
        /// Converts a string to the KebabCase where saces are replaced with dashes.
        /// </summary>
        /// <remarks>
        /// The returned string will always be lowercase. The method also inserts a dash between two capitalised letters, for example HelloWorld becomes hello-world.
        /// </remarks>
        /// <returns>The current string in Kebab case</returns>
        public static string? PascalToKebabCase(this string? value)
        {
            return value == null
                    ? null
                    : Regex.Replace(value,
                                     "([a-z])([A-Z])",
                                     "$1-$2",
                                     RegexOptions.CultureInvariant,
                                     TimeSpan.FromMilliseconds(100)).ToLowerInvariant().Replace(' ', '-');
        }

        /// <summary>
        /// Transform strings to Kebab case. This method places dashes between numbers.
        /// </summary>
        /// <remarks>
        /// This method is an alternative of <see cref="PascalToKebabCase(string?)"/>. It assumes the string is in Pascal case</remarks>
        /// <param name="source"></param>
        /// <returns>The current string in Kebab case.</returns>
        public static string? PascalToKebabCaseAlt(this string source)
        {
            if (source.Length == 0) return string.Empty;

            StringBuilder builder = new StringBuilder();

            for (var i = 0; i < source.Length; i++)
            {
                if (char.IsLower(source[i])) // if current char is already lowercase
                {
                    builder.Append(source[i]);
                }
                else if (i == 0) // if current char is the first char
                {
                    builder.Append(char.ToLower(source[i]));
                }
                else if (char.IsDigit(source[i]) && !char.IsDigit(source[i - 1])) // if current char is a number and the previous is not
                {
                    builder.Append('-');
                    builder.Append(source[i]);
                }
                else if (char.IsDigit(source[i])) // if current char is a number and previous is
                {
                    builder.Append(source[i]);
                }
                else if (char.IsLower(source[i - 1])) // if current char is upper and previous char is lower
                {
                    builder.Append('-');
                    builder.Append(char.ToLower(source[i]));
                }
                else if (i + 1 == source.Length || char.IsUpper(source[i + 1])) // if current char is upper and next char doesn't exist or is upper
                {
                    builder.Append(char.ToLower(source[i]));
                }
                else // if current char is upper and next char is lower
                {
                    builder.Append('-');
                    builder.Append(char.ToLower(source[i]));
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Transform text to Snake case, for example, 'Hello World' or 'HelloWorld' becomes 'hello_world'
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The input string in snake case</returns>
        public static string ToSnakeCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var builder = new StringBuilder(text.Length + Math.Min(2, text.Length / 5));
            var previousCategory = default(UnicodeCategory?);

            for (var currentIndex = 0; currentIndex < text.Length; currentIndex++)
            {
                var currentChar = text[currentIndex];
                if (currentChar == '_')
                {
                    builder.Append('_');
                    previousCategory = null;
                    continue;
                }

                var currentCategory = char.GetUnicodeCategory(currentChar);
                switch (currentCategory)
                {
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.TitlecaseLetter:
                        if (previousCategory == UnicodeCategory.SpaceSeparator ||
                            previousCategory == UnicodeCategory.LowercaseLetter ||
                            previousCategory != UnicodeCategory.DecimalDigitNumber &&
                            previousCategory != null &&
                            currentIndex > 0 &&
                            currentIndex + 1 < text.Length &&
                            char.IsLower(text[currentIndex + 1]))
                        {
                            builder.Append('_');
                        }

                        currentChar = char.ToLower(currentChar, CultureInfo.InvariantCulture);
                        break;

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (previousCategory == UnicodeCategory.SpaceSeparator)
                        {
                            builder.Append('_');
                        }
                        break;

                    default:
                        if (previousCategory != null)
                        {
                            previousCategory = UnicodeCategory.SpaceSeparator;
                        }
                        continue;
                }

                builder.Append(currentChar);
                previousCategory = currentCategory;
            }

            return builder.ToString();
        }
    }
}