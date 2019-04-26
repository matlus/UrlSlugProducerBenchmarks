namespace UrlSlugBenchmarks
{
    internal abstract class SlugProducerBase
    {
        /*
         * Percent-encoding, also known as URL encoding, is a mechanism for encoding information in a
         * Uniform Resource Identifier (URI) under certain circumstances. Although it is known as URL
         * encoding it is, in fact, used more generally within the main Uniform Resource Identifier
         * (URI) set, which includes both Uniform Resource Locator (URL) and Uniform Resource Name (URN).
         * As such, it is also used in the preparation of data of the application/x-www-form-urlencoded
         * media type, as is often used in the submission of HTML form data in HTTP requests.
         * http://en.wikipedia.org/wiki/Percent-encoding
        */

        protected const string s_slugValidCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
        protected const char s_longDash = '\u2013';
        protected const char s_dash = '-';
        protected const char s_period = '.';
        protected const char s_space = ' ';


        /// <summary>
        /// Given a string (Typically a title of a blog article or website) this class
        /// will produce a URL legal string by replacing illegal characters with nothing
        /// <para>
        /// There are some "Allowed" illegal characters that are replaced with a dash (minus sign)
        /// And they are: Space, Period, Dash, Long dash (unicode \u2013)
        /// </para>
        /// <para>
        /// If there are multiple contiguous "Allowed" illegal characters in the given string then
        /// all of them will be replaced by a single dash.
        /// </para>
        /// <para>
        /// If the resulting string starts with or ends with a dash then those dashes will be removed.
        ///  Thus the returned string will never start with or end with a dash.
        /// </para>
        /// <para>
        /// Further, all alpha characters will be converted to lower case
        /// </para>
        /// <para>
        /// Legal URL characters are - a through z and 0 through 9
        /// </para>
        /// </summary>
        /// <param name="title">The title of a blog post, video or description that can be used to produce a slug (A Legal Url)</param>
        /// <returns>Returns a Slug (A Url that contains Url Legal Characters)</returns>
        public string GetUrlSlug(string title)
        {
            return GetUrlSlugCore(title);
        }

        protected abstract string GetUrlSlugCore(string title);

    }
}
