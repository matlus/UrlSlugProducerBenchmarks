using System;
using System.Globalization;

namespace UrlSlugBenchmarks
{
    internal sealed class SlugProducerV2_2 : SlugProducerBase
    {
        protected override string GetUrlSlugCore(string title)
        {
            var value = title.ToLower(CultureInfo.CurrentCulture);
            var (cleanedUrlChars, desiredLength) = RemoveAndReplaceIllegalCharacters(value);
            return RemoveSpacesAndDashes(cleanedUrlChars, desiredLength);
        }

        private static (char[] cleanedUrlChars, int desiredLength) RemoveAndReplaceIllegalCharacters(string data)
        {
            var cleanedUrlChars = new char[data.Length];
            var previousWasDash = false;
            var currentIndex = 0;

            for (int i = 0; i < data.Length; i++)
            {
                var v = data[i];
                if (s_slugValidCharacters.IndexOf(v) >= 0)
                {
                    cleanedUrlChars[currentIndex++] = v;
                    previousWasDash = false;
                }
                else if (!previousWasDash && (v == s_space || v == s_period || v == s_dash || v == s_longDash))
                {
                    cleanedUrlChars[currentIndex++] = s_dash;
                    previousWasDash = true;
                }
            }

            return (cleanedUrlChars, currentIndex);
        }

        private static string RemoveSpacesAndDashes(char[] cleanedUrlChars, int desiredLength)
        {
            var startIndex = 0;

            if (cleanedUrlChars[0] == '-')
            {
                startIndex = 1;
            }

            if (cleanedUrlChars[desiredLength - 1] == '-')
            {
                desiredLength -= 1;
            }

            return new string(cleanedUrlChars, startIndex, desiredLength - startIndex);
        }
    }
}
