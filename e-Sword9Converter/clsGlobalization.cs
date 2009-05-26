
namespace eSword9Converter
{
    public class Globalization
    {
        public static LanguageBase CurrentLanguage;
        abstract public class LanguageBase
        {
            public abstract string MainTitle { get; }
            public abstract string AdvancedTitle { get; }
            public abstract string Convert { get; }
            public abstract string Source { get; }
            public abstract string Destination { get; }
            public abstract string ConvertedFile { get; }
            public abstract string FileToConvert { get; }
            public abstract string BatchMode { get; }
            public abstract string Bibles { get; }
            public abstract string Commentaries { get; }
            public abstract string Dictionaries { get; }
            public abstract string Harmonies { get; }
            public abstract string Bible { get; }
            public abstract string Commentary { get; }
            public abstract string Dictionary { get; }
            public abstract string Harmony { get; }
            public abstract string TopicNotes { get; }
            public abstract string VerseLists { get; }
            public abstract string Graphics { get; }
            public abstract string Notes { get; }
            public abstract string Illustrations { get; }
            public abstract string AlleSwordModules { get; }
            public abstract string AutomaticallyOverwrite { get; }
            public abstract string SkipPasswordProtectedFiles { get; }
            public abstract string IncludeSubdirectories { get; }
            public abstract string DestinationDirectory { get; }
            public abstract string SourceDirectory { get; }
            public abstract string Normal { get; }
            public abstract string Error { get; }
            public abstract string Warning { get; }
            public abstract string Row { get; }
            public abstract string Column { get; }
            public abstract string Type { get; }
            public abstract string Value { get; }
            public abstract string Message { get; }
            public abstract string Finished { get; }
            public abstract string InvalidPassword { get; }
            public abstract string Password { get; }
            public abstract string PasswordFound { get; }
            public abstract string Ok { get; }
            public abstract string Cancel { get; }
            public abstract string SourceFileNotExist { get; }
            public abstract string SourceFileInvalid { get; }
            public abstract string Loading { get; }
            public abstract string Converting { get; }
            public abstract string Saving { get; }
            public abstract string PasswordBlank { get; }
            public abstract string PrayerRequests { get; }
            public abstract string Overlay { get; }
            public abstract string MemoryVerses { get; }
            public abstract string BibleReadingPlan { get; }
            public abstract string Devotional { get; }
            public abstract string ProgressExceededMax { get; }
            public abstract string Completed { get; }
            public abstract string Overwrite { get; }
            public abstract string FileExists { get; }

            public string SaveFileList { get { return Bible + "|*.bblx|" + BibleReadingPlan + "|*.brpx|" + Commentary + "|*.cmtx|" + Dictionary + "|*.dctx|" + Harmony + "|*.harx|" + PrayerRequests + "|*.prlx" + TopicNotes + "|*.topx|" + VerseLists + "|*.lstx|" + Graphics + "|*.mapx|" + Notes + "|*.notx"; } }
            public string OpenFileList { get { return AlleSwordModules + "*.bbl;*.brp;*.cmt;*.dct;*.dev;*.map;*.har;*.not;*.mem;*.ovl;*.prl;*.top;*.lst|" + Bibles + "|*.bbl|" + Commentaries + "|*.cmt|" + Dictionaries + "|*.dct|" + Harmonies + "|*.har|" + TopicNotes + "|*.top|" + VerseLists + "|*.lst|" + Graphics + "|*.map|" + Notes + "|*.not"; } }
            public string sqlErrorString { get { return "{0}\t{1}\t" + Row + ":{2}\t" + Column + ":{3}\t" + Type + ":{4}\t" + Value + ":{5}\t" + Message + ":{6}"; } }
        }

        public class English : LanguageBase
        {
            public override string MainTitle { get { return "e-Sword 9 Converter"; } }
            public override string AdvancedTitle { get { return "e-Sword 9 Converter: Batch Mode"; } }
            public override string Convert { get { return "Convert"; } }
            public override string Source { get { return "Source"; } }
            public override string Destination { get { return "Destination"; } }
            public override string ConvertedFile { get { return "Converted File"; } }
            public override string FileToConvert { get { return "File to Convert"; } }
            public override string BatchMode { get { return "Batch Mode"; } }
            public override string Bibles { get { return "Bibles"; } }
            public override string Commentaries { get { return "Commentaries"; } }
            public override string Dictionaries { get { return "Dictionaries"; } }
            public override string Harmonies { get { return "Harmonies"; } }
            public override string TopicNotes { get { return "Topic Notes"; } }
            public override string VerseLists { get { return "Verse Lists"; } }
            public override string Graphics { get { return "Grraphics"; } }
            public override string Notes { get { return "Notes"; } }
            public override string Illustrations { get { return "Illustrations"; } }
            public override string AlleSwordModules { get { return "All e-Sword Modules"; } }
            public override string AutomaticallyOverwrite { get { return "Automatically Overwrite"; } }
            public override string SkipPasswordProtectedFiles { get { return "Skip Password Protected Files"; } }
            public override string IncludeSubdirectories { get { return "Include Subdirectories"; } }
            public override string DestinationDirectory { get { return "Destination Directory"; } }
            public override string SourceDirectory { get { return "Source Directory"; } }
            public override string Normal { get { return "Normal"; } }
            public override string Error { get { return "Error"; } }
            public override string Warning { get { return "Warning"; } }
            public override string Row { get { return "Row"; } }
            public override string Column { get { return "Column"; } }
            public override string Type { get { return "Type"; } }
            public override string Value { get { return "Value"; } }
            public override string Message { get { return "Message"; } }
            public override string Finished { get { return "Finished"; } }
            public override string InvalidPassword { get { return "Invalid Password"; } }
            public override string Password { get { return "Password"; } }
            public override string PasswordFound { get { return "Password for {0} is {1}"; } }
            public override string Ok { get { return "Ok"; } }
            public override string Cancel { get { return "Cancel"; } }
            public override string SourceFileNotExist { get { return "Source file does not exist!"; } }
            public override string SourceFileInvalid { get { return "Source file is invalid"; } }
            public override string Loading { get { return "Loading"; } }
            public override string Converting { get { return "Converting"; } }
            public override string Saving { get { return "Saving"; } }
            public override string PasswordBlank { get { return "Password cannot be blank"; } }
            public override string PrayerRequests { get { return "Prayer Requests"; } }
            public override string Overlay { get { return "Overlay"; } }
            public override string MemoryVerses { get { return "Memory Verses"; } }
            public override string BibleReadingPlan { get { return "Bible Reading Plan"; } }
            public override string Devotional { get { return "Devotional"; } }
            public override string ProgressExceededMax { get { return "Progress exceeded max allowed"; } }
            public override string Completed { get { return "Completed"; } }
            public override string Overwrite { get { return "do you want to overwrite?"; } }
            public override string FileExists { get { return "File Already Exists"; } }
            public override string Bible { get { return "Bible"; } }
            public override string Commentary { get { return "Commentary"; } }
            public override string Dictionary { get { return "Dictionary"; } }
            public override string Harmony { get { return "Harmony"; } }
        }
        public class Spanish : LanguageBase
        {
            public override string MainTitle { get { return "Convertidor e-Sword 9"; } }
            public override string AdvancedTitle { get { return "Convertidor e-Sword 9: Modo en lote"; } }
            public override string Convert { get { return "Convertir"; } }
            public override string Source { get { return "Origen"; } }
            public override string Destination { get { return "Destino"; } }
            public override string ConvertedFile { get { return "Archivo convertido"; } }
            public override string FileToConvert { get { return "Archivo a convertir"; } }
            public override string BatchMode { get { return "Modo en lote"; } }
            public override string Bibles { get { return "Biblias"; } }
            public override string Commentaries { get { return "Comentarios"; } }
            public override string Dictionaries { get { return "Dictionaries"; } }
            public override string Harmonies { get { return "Armonías"; } }
            public override string TopicNotes { get { return "Notas temáticas"; } }
            public override string VerseLists { get { return "Listas de versículos"; } }
            public override string Graphics { get { return "Gráficos"; } }
            public override string Notes { get { return "Notas"; } }
            public override string Illustrations { get { return "Ilustraciones"; } }
            public override string AlleSwordModules { get { return "Todos los módulos de e-Sword"; } }
            public override string AutomaticallyOverwrite { get { return "Sobrescribir automáticamente"; } }
            public override string SkipPasswordProtectedFiles { get { return "Saltar archivos protegidos"; } }
            public override string IncludeSubdirectories { get { return "Incluir subdirectorios"; } }
            public override string DestinationDirectory { get { return "Directorio de destino"; } }
            public override string SourceDirectory { get { return "Directorio de origen"; } }
            public override string Normal { get { return "Normal"; } }
            public override string Error { get { return "Error"; } }
            public override string Warning { get { return "Advertencia"; } }
            public override string Row { get { return "Fila"; } }
            public override string Column { get { return "Columna"; } }
            public override string Type { get { return "Tipo"; } }
            public override string Value { get { return "Valor"; } }
            public override string Message { get { return "Mensaje"; } }
            public override string Finished { get { return "Finalizado"; } }
            public override string InvalidPassword { get { return "Contraseña inválida"; } }
            public override string Password { get { return "Contraseña"; } }
            public override string PasswordFound { get { return "Contraseña para {0} es {1}"; } }
            public override string Ok { get { return "Correcto"; } }
            public override string Cancel { get { return "Cancelar"; } }
            public override string Loading { get { return "Cargando"; } }
            public override string Converting { get { return "Convirtiendo"; } }
            public override string Saving { get { return "Guardando"; } }
            public override string PasswordBlank { get { return "Debe escribir la contraseña"; } }
            public override string PrayerRequests { get { return "Peticiones de oración"; } }
            public override string Overlay { get { return "Superposición"; } }
            public override string MemoryVerses { get { return "Versos a memorizar"; } }
            public override string BibleReadingPlan { get { return "Plan de lectura Bíblica"; } }
            public override string Devotional { get { return "Devocional"; } }
            public override string ProgressExceededMax { get { return "El proceso rebasó el máximo permitido"; } }
            public override string Completed { get { return "Completado"; } }
            public override string Overwrite { get { return "¿Quieres sobrescribirlo?"; } }
            public override string FileExists { get { return "Ya existe este archivo"; } }
            public override string SourceFileNotExist { get { return "Archivo de origen no existen"; } }
            public override string SourceFileInvalid { get { return "Archivo de origen es inválido"; } }
            public override string Bible { get { return "Biblia"; } }
            public override string Commentary { get { return "Comentario"; } }
            public override string Dictionary { get { return "Dictionarie"; } }
            public override string Harmony { get { return "Armonía"; } }
        }
    }
}