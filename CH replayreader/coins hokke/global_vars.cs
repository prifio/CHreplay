using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coins_hockey
{
    class Z
    {
        public static cointype[] tcoin;
        public static void init()
        {
            tcoin = new cointype[]{
                new cointype(15, 150, 1000000, "1 копейка", "1ko", "1kr"),
            new cointype(19, 260, 5, "5 копеек", "5ko", "5kr"),
            new cointype(18, 185, 10, "10 копеек", "10ko", "10kr"),
            new cointype(20, 275, 50, "50 копеек", "50ko", "50kr"),
            new cointype(21, 300, 100, "1 рубль", "1ro", "1rr"),
            new cointype(23, 500, 200, "2 рубля", "2ro", "2rr"),
            new cointype(25, 600, 500, "5 рублей", "5ro", "5rr"),
            new cointype(22, 563, 1000, "10 рублей", "10ro", "10rr"),
            new cointype(19, 250, 50, "1 цент", "1co", "1cr"),
            new cointype(21, 500, 250, "5 центов", "5co", "5cr"),
            new cointype(18, 226, 500, "10 центов", "10co", "10cr"),
            new cointype(24, 567, 1250, "25 центов", "25co", "25cr"),
            new cointype(27, 810, 5000, "1 доллар", "1so", "1sr"),
            new cointype(16, 230, 53, "1 евроцент", "1eco", "1ecr"),
            new cointype(19, 306, 106, "2 евроцента", "2eco", "2ecr"),
            new cointype(20, 410, 530, "10 евроцентов", "10eco", "10ecr"),
            new cointype(22, 574, 1060, "20 евроцентов", "20eco", "20ecr"),
            new cointype(24, 780, 2650, "50 евроцентов", "50eco", "50ecr"),
            new cointype(23, 750, 5300, "1 евро", "1eo", "1er"),
            new cointype(26, 850, 10600, "2 евро", "2eo", "2er"),
            new cointype(20, 356, 75, "1 penny", "1po", "1pr"),
            new cointype(26, 712, 150, "2 penny", "2po", "2pr"),
            new cointype(18, 325, 375, "5 penny", "5po", "5pr"),
            new cointype(25, 650, 750, "10 penny", "10po", "10pr"),
            new cointype(23, 950, 7500, "1 pound", "1fo", "1fr"),
            new cointype(28, 1200, 15000, "2 pound", "2fo", "2fr")
        };
        }
        public static int radangl = 100;
        public static int clwidth = 100, clheight = 100;
    }
}
