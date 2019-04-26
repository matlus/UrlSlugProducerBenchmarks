using System.Globalization;

namespace UrlSlugBenchmarks
{
    internal sealed class SlugProducerV2 : SlugProducerBase
    {
        protected override string GetUrlSlugCore(string title)
        {
            var (cleanedUrlChars, desiredLength) = RemoveAndReplaceIllegalCharacters(title);
            return RemoveSpacesAndDashes(cleanedUrlChars, desiredLength);
        }

        private static (char[] cleanedUrlChars, int desiredLength) RemoveAndReplaceIllegalCharacters(string data)
        {
            var cleanedUrlChars = new char[data.Length];
            var previousWasDash = false;
            var currentIndex = 0;

            for (int i = 0; i < data.Length; i++)
            {
                var v = char.ToLower(data[i], CultureInfo.CurrentCulture);
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
            var slugArray = new char[desiredLength];
            var endIndex = 0;

            for (int i = 0; i < slugArray.Length; i++)
            {
                var v = cleanedUrlChars[i];
                if (v != s_space)
                {
                    slugArray[endIndex++] = v;
                }
            }

            var startIndex = 0;
            if (slugArray[0] == s_dash)
            {
                startIndex = 1;
            }

            if (slugArray[endIndex - 1] == s_dash)
            {
                endIndex--;
            }

            return new string(slugArray, startIndex, endIndex - startIndex);
        }
    }
}