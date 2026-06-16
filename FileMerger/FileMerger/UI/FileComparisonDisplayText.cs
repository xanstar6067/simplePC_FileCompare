using FileMerger.Models;

namespace FileMerger.UI
{
    internal static class FileComparisonDisplayText
    {
        public static string GetStatusText(FileComparisonResult result)
        {
            return result.Status switch
            {
                FileComparisonStatus.MissingInA => "Нет в A",
                FileComparisonStatus.MissingInB => "Нет в B",
                FileComparisonStatus.Different => "Различается",
                FileComparisonStatus.Same => "Совпадает",
                FileComparisonStatus.ReadError => "Ошибка чтения",
                _ => "Неизвестно"
            };
        }

        public static string FormatSize(long? size)
        {
            if (size is null)
            {
                return "-";
            }

            string[] units = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
            double value = size.Value;
            int unitIndex = 0;

            while (value >= 1024 && unitIndex < units.Length - 1)
            {
                value /= 1024;
                unitIndex++;
            }

            return unitIndex == 0
                ? $"{value:N0} {units[unitIndex]}"
                : $"{value:N1} {units[unitIndex]}";
        }

        public static string FormatDate(DateTime? date)
        {
            return date?.ToString("dd.MM.yyyy HH:mm") ?? "-";
        }
    }
}
