﻿using BulkCrapUninstaller.Properties;
using Klocman.Extensions;
using Klocman.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace BulkCrapUninstaller
{
    public static class CultureConfigurator
    {
        private static IEnumerable<CultureInfo> _supportedLanguages;
        private static CultureInfo _enUsCulture;

        private static CultureInfo EnUsCulture => 
            _enUsCulture ?? (_enUsCulture = CultureInfo.GetCultureInfo("en-US"));

        public static IEnumerable<CultureInfo> SupportedLanguages => 
            _supportedLanguages ?? (_supportedLanguages = GetSupportedLanguages());

        private static IEnumerable<CultureInfo> GetSupportedLanguages()
        {
            return new[]
            {
                // en - English
                EnUsCulture, //CultureInfo.GetCultureInfo("en-US"),
                CultureInfo.GetCultureInfo("en-AU"),
                CultureInfo.GetCultureInfo("en-BZ"),
                CultureInfo.GetCultureInfo("en-CA"),
                CultureInfo.GetCultureInfo("en-IE"),
                CultureInfo.GetCultureInfo("en-JM"),
                CultureInfo.GetCultureInfo("en-NZ"),
                CultureInfo.GetCultureInfo("en-PH"),
                CultureInfo.GetCultureInfo("en-ZA"),
                CultureInfo.GetCultureInfo("en-TT"),
                CultureInfo.GetCultureInfo("en-GB"),
                CultureInfo.GetCultureInfo("en-ZW"),

                // de - German
                CultureInfo.GetCultureInfo("de-AT"),
                CultureInfo.GetCultureInfo("de-DE"),
                CultureInfo.GetCultureInfo("de-LI"),
                CultureInfo.GetCultureInfo("de-LU"),
                CultureInfo.GetCultureInfo("de-CH"),

                // es - Spanish
                CultureInfo.GetCultureInfo("es-AR"),
                CultureInfo.GetCultureInfo("es-BO"),
                CultureInfo.GetCultureInfo("es-CL"),
                CultureInfo.GetCultureInfo("es-CO"),
                CultureInfo.GetCultureInfo("es-CR"),
                CultureInfo.GetCultureInfo("es-DO"),
                CultureInfo.GetCultureInfo("es-EC"),
                CultureInfo.GetCultureInfo("es-SV"),
                CultureInfo.GetCultureInfo("es-GT"),
                CultureInfo.GetCultureInfo("es-HN"),
                CultureInfo.GetCultureInfo("es-MX"),
                CultureInfo.GetCultureInfo("es-NI"),
                CultureInfo.GetCultureInfo("es-PA"),
                CultureInfo.GetCultureInfo("es-PY"),
                CultureInfo.GetCultureInfo("es-PE"),
                CultureInfo.GetCultureInfo("es-PR"),
                CultureInfo.GetCultureInfo("es-ES"),
                CultureInfo.GetCultureInfo("es-UY"),
                CultureInfo.GetCultureInfo("es-VE"),

                // fr - French
                CultureInfo.GetCultureInfo("fr-BE"),
                CultureInfo.GetCultureInfo("fr-CA"),
                CultureInfo.GetCultureInfo("fr-FR"),
                CultureInfo.GetCultureInfo("fr-LU"),
                CultureInfo.GetCultureInfo("fr-MC"),
                CultureInfo.GetCultureInfo("fr-CH"),

                // it - Italian
                CultureInfo.GetCultureInfo("it-IT"),
                CultureInfo.GetCultureInfo("it-CH"),

                // pt - Portuguese
                CultureInfo.GetCultureInfo("pt-PT"),
                CultureInfo.GetCultureInfo("pt-BR"),

                // nl - Dutch
                CultureInfo.GetCultureInfo("nl-NL"),
                CultureInfo.GetCultureInfo("nl-BE"),

                // Misc languages
                CultureInfo.GetCultureInfo("cs-CZ"),
                CultureInfo.GetCultureInfo("hu-HU"),
                CultureInfo.GetCultureInfo("pl-PL"),
                CultureInfo.GetCultureInfo("ru-RU"),
                CultureInfo.GetCultureInfo("sl-SI")
            }.OrderBy(x => x.DisplayName).ToList().AsEnumerable();
        }

        public static void SetupCulture()
        {
            var currentCulture = CultureInfo.CurrentCulture;

            var targetLocale = Settings.Default.Language;
            if (targetLocale.IsNotEmpty())
            {
                try
                {
                    currentCulture = SupportedLanguages.First(x => x.Name.Equals(targetLocale));
                }
                catch
                {
                    Settings.Default.Language = string.Empty;
                }
            }

            if (!currentCulture.Name.ContainsAny(SupportedLanguages.Select(x => x.Parent.Name),
                StringComparison.OrdinalIgnoreCase))
                currentCulture = EnUsCulture;

            ProcessTools.SetDefaultCulture(currentCulture);
            var thread = Thread.CurrentThread;
            thread.CurrentCulture = currentCulture;
            thread.CurrentUICulture = currentCulture;
        }
    }
}
