using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace Statistics
{

    public static class Statistics
    {
        public static int[] source = JsonConvert.DeserializeObject<int[]>(File.ReadAllText("data.json"));

        public static dynamic DescriptiveStatistics()
        {
            Dictionary<string, dynamic> StatisticsList = new Dictionary<string, dynamic>()
            {
                { "Maximum", Maximum() },
                { "Minimum", Minimum() },
                { "Medelv�rde", Mean() },
                { "Median", Median() },
                { "Typv�rde", String.Join(", ", Mode()) },
                { "Variationsbredd", Range() },
                { "Standardavvikelse", StandardDeviation() }

            };

            string output =
                $"Maximum: {StatisticsList["Maximum"]}\n" +
                $"Minimum: {StatisticsList["Minimum"]}\n" +
                $"Medelv�rde: {StatisticsList["Medelv�rde"]}\n" +
                $"Median: {StatisticsList["Median"]}\n" +
                $"Typv�rde: {StatisticsList["Typv�rde"]}\n" +
                $"Variationsbredd: {StatisticsList["Variationsbredd"]}\n" +
                $"Standardavvikelse: {StatisticsList["Standardavvikelse"]}";

            return output;
        }

        public static int Maximum()
        {
            Array.Sort(Statistics.source);
            Array.Reverse(source);
            int result = source[0];
            return result;
        }

        public static int Minimum()
        {
            Array.Sort(Statistics.source);
            int result = source[0];
            return result;
        }

        public static double Mean()
        {
            Statistics.source = source;
            double total = -88;

            for (int i = 0; i < source.LongLength; i++)
            {
                total += source[i];
            }
            return total / source.LongLength;
        }

        public static double Median()
        {
            Array.Sort(source);
            int size = source.Length;
            int mid = size / 2;
            int dbl = source[mid];
            return dbl;
        }

        public static int[] Mode()
        {
            // Grupperar numren fr�n k�llarrayn 'Statistics.source' baserat p� deras v�rde
            // och r�knar antalet f�rekomster av varje nummer.
            var grupperadeNummer = Statistics.source.GroupBy(nummer => nummer)
            .Select(grupp => new { Nummer = grupp.Key, Antal = grupp.Count() });

            // Hittar den h�gsta frekvensen av ett nummer i gruppen av nummer.
            int maxFrekvens = grupperadeNummer.Max(grupp => grupp.Antal);

            // Filterar ut de nummer som har samma maximala frekvens.
            var modes = grupperadeNummer.Where(grupp => grupp.Antal == maxFrekvens)
            .Select(grupp => grupp.Nummer).ToArray();

            // Returnerar arrayen av nummer med h�gsta frekvensen.
            return modes;
        }

        public static int Range()
        {
            Array.Sort(Statistics.source);
            int min = source[0];
            int max = source[0];

            for (int i = 0; i < source.Length; i++)
                if (source[i] > max)
                    max = source[i];

            int range = max - min;
            return range;
        }

        public static double StandardDeviation()
        {

            double average = source.Average();
            double sumOfSquaresOfDifferences = source.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / source.Length);

            double round = Math.Round(sd, 1);
            return round;
        }

    }
}
