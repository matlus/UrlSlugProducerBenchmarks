using System.Text;
using System.Globalization;

namespace UrlSlugBenchmarks
{
    internal sealed class SlugProducerV1 : SlugProducerBase
    {
        protected override string GetUrlSlugCore(string title)
        {
            var stringBuilder = new StringBuilder();
            var value = title.ToLower(CultureInfo.CurrentCulture).Trim();

            stringBuilder = RemoveAndReplaceIllegalCharacters(stringBuilder, value);
            return RemoveStartAndEndDashes(stringBuilder);
        }

        private static StringBuilder RemoveAndReplaceIllegalCharacters(StringBuilder stringBuilder, string value)
        {
            var previousWasDash = false;
            for (int i = 0; i < value.Length; i++)
            {
                var v = value[i];
                if (s_slugValidCharacters.IndexOf(v) >= 0)
                {
                    stringBuilder.Append(v);
                    previousWasDash = false;
                }
                else if (!previousWasDash && (v == s_space || v == s_period || v == s_dash || v == s_longDash))
                {
                    stringBuilder.Append(s_dash);
                    previousWasDash = true;
                }
            }

            return stringBuilder;
        }

        private static string RemoveStartAndEndDashes(StringBuilder stringBuilder)
        {
            var startIndex = 0;
            var length = stringBuilder.Length;

            if (stringBuilder[0] == '-')
            {
                startIndex = 1;
            }

            if (stringBuilder[stringBuilder.Length - 1] == '-')
            {
                length -= 1;
            }

            return stringBuilder.ToString(startIndex, length - startIndex);
        }
    }
}
