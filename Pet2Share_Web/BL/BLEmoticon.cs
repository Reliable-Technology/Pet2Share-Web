using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.BL
{
    public sealed class BLEmoticon
    {

        private static readonly BLEmoticon instance = new BLEmoticon();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BLEmoticon()
        {
        }

        private BLEmoticon()
        {
        }

        public static BLEmoticon Instance
        {
            get
            {
                return instance;
            }
        }

        public BLEmoticon[] All
        {
            get
            {
                ArrayList result = new ArrayList();

                // --
                // Actually add the emoticons.

                // MSN Messenger 4.5.
                result.Add(new BLEmoticon("thumbs_up.gif", "(Y)", "Thumbs up"));
                result.Add(new BLEmoticon("thumbs_down.gif", "(N)", "Thumbs down"));
                result.Add(new BLEmoticon("beer_yum.gif", "(B)", "Beer"));
                result.Add(new BLEmoticon("martini_shaken.gif", "(D)", "Martini Glass"));
                result.Add(new BLEmoticon("girl_handsacrossamerica.gif", "(X)", "Girl"));
                result.Add(new BLEmoticon("guy_handsacrossamerica.gif", "(Z)", "Boy"));
                result.Add(new BLEmoticon("bat.gif", ":-[", "Bat"));
                result.Add(new BLEmoticon("girl_hug.gif", "(})", "Hug right"));
                result.Add(new BLEmoticon("dude_hug.gif", "({)", "Hug left"));
                result.Add(new BLEmoticon("regular_smile.gif", ":-)", "smiley"));
                result.Add(new BLEmoticon("teeth_smile.gif", ":-D", "Theeth smiley"));
                result.Add(new BLEmoticon("omg_smile.gif", ":-O", "OMG smiley"));
                result.Add(new BLEmoticon("tounge_smile.gif", ":-P", "Tounge smiley"));
                result.Add(new BLEmoticon("wink_smile.gif", ";-)", "Wink smiley"));
                result.Add(new BLEmoticon("sad_smile.gif", ":-)", "Sad smiley"));
                result.Add(new BLEmoticon("confused_smile.gif", ":-S", "Confused smiley"));
                result.Add(new BLEmoticon("whatchutalkingabout_smile.gif", ":-|", "Serious smiley"));
                result.Add(new BLEmoticon("cry_smile.gif", ":'(", "Crying smiley"));
                result.Add(new BLEmoticon("cry_smile.gif", ":-(", "Crying smiley"));
                result.Add(new BLEmoticon("embaressed_smile.gif", ":$", "Embaressed smiley"));
                result.Add(new BLEmoticon("shades_smile.gif", "(H)", "smiley with shades"));
                result.Add(new BLEmoticon("angry_smile.gif", ":-@", "Angry smiley"));
                result.Add(new BLEmoticon("angel_smile.gif", "(A)", "Angel smiley"));
                result.Add(new BLEmoticon("devil_smile.gif", "(6)", "Devil smiley"));
                result.Add(new BLEmoticon("heart.gif", "(L)", "Red heart"));
                result.Add(new BLEmoticon("broken_heart.gif", "(U)", "Broken heart"));
                result.Add(new BLEmoticon("kiss.gif", "(K)", "Red lips"));
                result.Add(new BLEmoticon("present.gif", "(G)", "Present"));
                result.Add(new BLEmoticon("rose.gif", "(F)", "Red rose"));
                result.Add(new BLEmoticon("wilted_rose.gif", "(W)", "Wilted rose"));
                result.Add(new BLEmoticon("camera.gif", "(P)", "Camera"));
                result.Add(new BLEmoticon("film.gif", "(~)", "Film"));
                result.Add(new BLEmoticon("phone.gif", "(T)", "Phone"));
                result.Add(new BLEmoticon("kittykay.gif", "(@)", "Cat"));
                result.Add(new BLEmoticon("bowwow.gif", "(&)", "Dog"));
                result.Add(new BLEmoticon("coffee.gif", "(C)", "Coffee"));
                result.Add(new BLEmoticon("lightbulb.gif", "(I)", "Light bulb"));
                result.Add(new BLEmoticon("moon.gif", "(S)", "Moon"));
                result.Add(new BLEmoticon("musical_note.gif", "(8)", "Musical note"));
                result.Add(new BLEmoticon("envelope_open.gif", "(OE)", "Open envelope"));
                result.Add(new BLEmoticon("cake.gif", "(^)", "Cake"));
                result.Add(new BLEmoticon("clock.gif", "(O)", "Clock"));
                result.Add(new BLEmoticon("rainbow.gif", "(R)", "Rainbow"));
                result.Add(new BLEmoticon("sun.gif", "(#)", "Sun"));
                result.Add(new BLEmoticon("questionmark.gif", "(?)", "Questionmark"));
                result.Add(new BLEmoticon("hs.gif", "(%)", "Handcuff"));

                // Some of MSN Messenger 6.																	 							
                result.Add(new BLEmoticon("envelope.gif", "(E)", "Envelope"));
                result.Add(new BLEmoticon("pizza.gif", "(PI)", "Pizza"));
                result.Add(new BLEmoticon("soccer_ball.gif", "(SO)", "Soccer ball"));
                result.Add(new BLEmoticon("money.gif", "(MO)", "Money"));
                result.Add(new BLEmoticon("island.gif", "(IP)", "Island"));
                result.Add(new BLEmoticon("plane.gif", "(AP)", "Plane"));
                result.Add(new BLEmoticon("auto.gif", "(AU)", "Car"));
                result.Add(new BLEmoticon("mobile_phone.gif", "(MP)", "Mobile phone"));
                result.Add(new BLEmoticon("sheep.gif", "(BAH)", "Sheep"));
                result.Add(new BLEmoticon("snail.gif", "(SN)", "Snail"));

                // Some of my own.

                result.Add(new BLEmoticon("uwe.gif", "(Uwe)", "The Uwe (<--clever!)", "http://www.magerquark.de"));
                result.Add(new BLEmoticon("harald.gif", "(Harald)", "The Harald (www.geisselhart.de)", "http://www.geisselhart.de"));
                result.Add(new BLEmoticon("johanna.gif", "(Johanna)", "The Johanna (www.kuhnijunior.de)", "http://www.kuhnijunior.de"));
                result.Add(new BLEmoticon("andreas.gif", "(Andreas)", "The Andreas (www.kuhni.de)", "http://www.kuhni.de"));
                result.Add(new BLEmoticon("klettern.gif", "(Climbing)", "Climbing"));
                result.Add(new BLEmoticon("geburtstag.gif", "(Birthday cake)", "Birthday cake"));
                result.Add(new BLEmoticon("t19.gif", "(:)", ""));
                result.Add(new BLEmoticon("zeta-producer.gif", "(ZP)", "zeta producer", "http://www.zeta-producer.de"));
                result.Add(new BLEmoticon("m3u.gif", "(WinAmp)", "Winamp"));
                result.Add(new BLEmoticon("new.gif", "(New)", "New"));

                // --

                if (result.Count == 0)
                    return null;
                else
                    return (BLEmoticon[])result.ToArray(typeof(BLEmoticon));
            }
        }

        /// <summary>
        /// Returns a string with all emoticons replaced by their images.
        /// </summary>
        public string Format(string input)
        {
            if (input == null || input.Length == 0)
            {
                return input;
            }
            else
            {
                string result = input;

                BLEmoticon[] all = BLEmoticon.Instance.All;
                foreach (BLEmoticon emoticon in all)
                {
                    string a;
                    string a_;
                    int border;

                    // Decide whether a link is required.
                    if (emoticon.Url != null && emoticon.Url.Length > 0)
                    {
                        a = string.Format("<a href=\"{0}\">", emoticon.Url);
                        a_ = "</a>";
                        border = 1;
                    }
                    else
                    {
                        a = "";
                        a_ = "";
                        border = 0;
                    }

                    // Replace this emoticon.
                    string replacement =
                        string.Format(
                        "{0}<img src=\"{1}\" alt=\"{2}\" align=\"AbsMiddle\" border=\"{3}\" />{4}",
                        a,
                        emoticon.VirtualPath,
                        HttpUtility.HtmlEncode(emoticon.Title),
                        border,
                        a_);

                    result = result.Replace(emoticon.Shortcut, replacement);
                }

                return result;
            }
        }


        #region Constructors.
        // ------------------------------------------------------------------

        public BLEmoticon(BLEmoticon src)
        {
            Shortcut = src.Shortcut;
            Filename = src.Filename;
            Title = src.Title;
            Url = src.Url;

            Check();
        }

        public BLEmoticon(string filename, string shortcut)
        {
            Shortcut = shortcut;
            Filename = filename;

            Check();
        }

        public BLEmoticon(string filename, string shortcut, string title)
        {
            Shortcut = shortcut;
            Filename = filename;
            Title = title;

            Check();
        }

        public BLEmoticon(string filename, string shortcut, string title, string url)
        {
            Shortcut = shortcut;
            Filename = filename;
            Title = title;
            Url = url;

            Check();
        }

        // ------------------------------------------------------------------
        #endregion

        #region Properties.
        // ------------------------------------------------------------------

        /// <summary>
        /// The (case-sensitive!) string to be replaced with the emoticon.
        /// </summary>
        public string Shortcut = "";

        /// <summary>
        /// The filename (no path) of the emoticon.
        /// </summary>
        public string Filename = "";

        /// <summary>
        /// The optional tooltip of the emoticon.
        /// </summary>
        public string Title = "";

        /// <summary>
        /// The optional URL of the emoticon. If specified, the emoticon
        /// can be clicked.
        /// </summary>
        public string Url = "";

        // ------------------------------------------------------------------
        #endregion

        #region Internal helper.
        // ------------------------------------------------------------------

        /// <summary>
        /// Returns the complete virtual path.
        /// </summary>
        public string VirtualPath
        {
            get
            {
                string path = "~/Emoticons/" + Filename;
                return ReplaceTilde(path);
            }
        }

        /// <summary>
        /// Get the root of the current web application.
        /// Expands a "~" character by the real path.
        /// </summary>
        private static string ReplaceTilde(string path)
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
                return path.Replace("~", "");
            else
                return path.Replace("~", HttpContext.Current.Request.ApplicationPath);
        }

        /// <summary>
        /// Do member-checking, whether it is valid.
        /// </summary>
        private void Check()
        {
            if (Shortcut == null || Shortcut.Length == 0)
                throw new ArgumentException("BLEmoticon.Shortcut must be non-empty", "BLEmoticon.Shortcut");
            if (Filename == null || Filename.Length == 0)
                throw new ArgumentException("BLEmoticon.Filename must be non-empty", "BLEmoticon.Filename");
        }

        // ------------------------------------------------------------------
        #endregion
    }

    /////////////////////////////////////////////////////////////////////////
}
