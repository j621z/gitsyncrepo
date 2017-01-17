﻿using System.Collections.Generic;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
    public class SurnameGenerator
        : DataElementGenerator<string, SurnameGenerationOptions>
    {
        private static readonly WeightedRandomizer<string> WeightedRandomizer = new WeightedRandomizer<string>();

        static SurnameGenerator()
        {
            WeightedRandomizer.Add(DataElementGenerator.Values.Surnames);
        }

        public SurnameGenerator()
            : base(SurnameGenerationOptions.Default)
        {
        }

        public SurnameGenerator(SurnameGenerationOptions options)
            : base(options)
        {
        }

        public override string Generate(SurnameGenerationOptions options)
        {
            return options.UseWeightedGeneration ? WeightedRandomizer.Next() : DataElementGenerator.Values.Surnames.RandomKey();
        }
    }

    public class SurnameGenerationOptions
        : DataElementGenerationOptions
    {
        public bool UseWeightedGeneration { get; set; }

        public static SurnameGenerationOptions Default => new SurnameGenerationOptions { UseWeightedGeneration = true };
    }

    public static partial class DataElementGenerator
    {
        public static partial class Values
        {
            public static Dictionary<string, int> Surnames = new Dictionary<string, int>()
            {
                { "Smith", 2376206 },
                { "Johnson", 1857160 },
                { "Williams", 1534042 },
                { "Brown", 1380145 },
                { "Jones", 1362755 },
                { "Miller", 1127803 },
                { "Davis", 1072335 },
                { "Garcia", 858289 },
                { "Rodriguez", 804240 },
                { "Wilson", 783051 },
                { "Martinez", 775072 },
                { "Anderson", 762394 },
                { "Taylor", 720370 },
                { "Thomas", 710696 },
                { "Hernandez", 706372 },
                { "Moore", 698671 },
                { "Martin", 672711 },
                { "Jackson", 666125 },
                { "Thompson", 644368 },
                { "White", 639515 },
                { "Lopez", 621536 },
                { "Lee", 605860 },
                { "Gonzalez", 597718 },
                { "Harris", 593542 },
                { "Clark", 548369 },
                { "Lewis", 509930 },
                { "Robinson", 503028 },
                { "Walker", 501307 },
                { "Perez", 488521 },
                { "Hall", 473568 },
                { "Young", 465948 },
                { "Allen", 463368 },
                { "Sanchez", 441242 },
                { "Wright", 440367 },
                { "King", 438986 },
                { "Scott", 420091 },
                { "Green", 413477 },
                { "Baker", 413351 },
                { "Adams", 413086 },
                { "Nelson", 412236 },
                { "Hill", 411770 },
                { "Ramirez", 388987 },
                { "Campbell", 371953 },
                { "Mitchell", 367433 },
                { "Roberts", 366215 },
                { "Carter", 362548 },
                { "Phillips", 351848 },
                { "Evans", 342237 },
                { "Turner", 335663 },
                { "Torres", 325169 },
                { "Parker", 324246 },
                { "Collins", 317848 },
                { "Edwards", 317070 },
                { "Stewart", 312899 },
                { "Flores", 312615 },
                { "Morris", 311754 },
                { "Nguyen", 310125 },
                { "Murphy", 300501 },
                { "Rivera", 299463 },
                { "Cook", 294795 },
                { "Rogers", 294403 },
                { "Morgan", 276400 },
                { "Peterson", 275041 },
                { "Cooper", 270097 },
                { "Reed", 267443 },
                { "Bailey", 265916 },
                { "Bell", 264752 },
                { "Gomez", 263590 },
                { "Kelly", 260385 },
                { "Howard", 254779 },
                { "Ward", 254121 },
                { "Cox", 253771 },
                { "Diaz", 251772 },
                { "Richardson", 249533 },
                { "Wood", 247299 },
                { "Watson", 242432 },
                { "Brooks", 240751 },
                { "Bennett", 239055 },
                { "Gray", 236713 },
                { "James", 233224 },
                { "Reyes", 232511 },
                { "Cruz", 231065 },
                { "Hughes", 229390 },
                { "Price", 228756 },
                { "Myers", 224824 },
                { "Long", 223494 },
                { "Foster", 221040 },
                { "Sanders", 220902 },
                { "Ross", 219961 },
                { "Morales", 217642 },
                { "Powell", 216553 },
                { "Sullivan", 215640 },
                { "Russell", 215432 },
                { "Ortiz", 214683 },
                { "Jenkins", 213737 },
                { "Gutierrez", 212905 },
                { "Perry", 212644 },
                { "Butler", 210879 },
                { "Barnes", 210426 },
                { "Fisher", 210279 },
                { "Henderson", 210094 },
                { "Coleman", 208624 },
                { "Simmons", 201650 },
                { "Patterson", 198557 },
                { "Jordan", 197212 },
                { "Reynolds", 195598 },
                { "Hamilton", 194331 },
                { "Graham", 194074 },
                { "Kim", 194067 },
                { "Gonzales", 193934 },
                { "Alexander", 193443 },
                { "Ramos", 193096 },
                { "Wallace", 190760 },
                { "Griffin", 190636 },
                { "West", 188464 },
                { "Cole", 187793 },
                { "Hayes", 187473 },
                { "Chavez", 185865 },
                { "Gibson", 184420 },
                { "Bryant", 183761 },
                { "Ellis", 181934 },
                { "Stevens", 181417 },
                { "Murray", 178414 },
                { "Ford", 178397 },
                { "Marshall", 177213 },
                { "Owens", 176334 },
                { "Mcdonald", 176094 },
                { "Harrison", 175577 },
                { "Ruiz", 175429 },
                { "Kennedy", 171636 },
                { "Wells", 170635 },
                { "Alvarez", 168817 },
                { "Woods", 168814 },
                { "Mendoza", 168567 },
                { "Castillo", 165473 },
                { "Olson", 163502 },
                { "Webb", 163481 },
                { "Washington", 163036 },
                { "Tucker", 162933 },
                { "Freeman", 162686 },
                { "Burns", 162153 },
                { "Henry", 161392 },
                { "Vasquez", 159989 },
                { "Snyder", 159363 },
                { "Simpson", 158241 },
                { "Crawford", 158121 },
                { "Jimenez", 157475 },
                { "Porter", 156848 },
                { "Mason", 155484 },
                { "Shaw", 155172 },
                { "Gordon", 154934 },
                { "Wagner", 154516 },
                { "Hunter", 154410 },
                { "Romero", 153772 },
                { "Hicks", 153618 },
                { "Dixon", 152015 },
                { "Hunt", 151986 },
                { "Palmer", 150407 },
                { "Robertson", 150299 },
                { "Black", 150186 },
                { "Holmes", 150166 },
                { "Stone", 149802 },
                { "Meyer", 149664 },
                { "Boyd", 149476 },
                { "Mills", 147909 },
                { "Warren", 147906 },
                { "Fox", 147357 },
                { "Rose", 146924 },
                { "Rice", 146440 },
                { "Moreno", 146088 },
                { "Schmidt", 145565 },
                { "Patel", 145066 },
                { "Ferguson", 142256 },
                { "Nichols", 141936 },
                { "Herrera", 140786 },
                { "Medina", 139353 },
                { "Ryan", 139335 },
                { "Fernandez", 139302 },
                { "Weaver", 138811 },
                { "Daniels", 138776 },
                { "Stephens", 138742 },
                { "Gardner", 138660 },
                { "Payne", 138028 },
                { "Kelley", 137555 },
                { "Dunn", 136955 },
                { "Pierce", 136517 },
                { "Arnold", 136315 },
                { "Tran", 136095 },
                { "Spencer", 134443 },
                { "Peters", 134231 },
                { "Hawkins", 134066 },
                { "Grant", 134034 },
                { "Hansen", 133474 },
                { "Castro", 133254 },
                { "Hoffman", 132645 },
                { "Hart", 132466 },
                { "Elliott", 132457 },
                { "Cunningham", 131896 },
                { "Knight", 131860 },
                { "Bradley", 131289 },
                { "Carroll", 131020 },
                { "Hudson", 130793 },
                { "Duncan", 130419 },
                { "Armstrong", 129982 },
                { "Berry", 129405 },
                { "Andrews", 129320 },
                { "Johnston", 128935 },
                { "Ray", 128794 },
                { "Lane", 128727 },
                { "Riley", 127960 },
                { "Carpenter", 127073 },
                { "Perkins", 126951 },
                { "Aguilar", 126399 },
                { "Silva", 126164 },
                { "Richards", 125653 },
                { "Willis", 125627 },
                { "Matthews", 124839 },
                { "Chapman", 124614 },
                { "Lawrence", 124321 },
                { "Garza", 124130 },
                { "Vargas", 123952 },
                { "Watkins", 122447 },
                { "Wheeler", 121684 },
                { "Larson", 121064 },
                { "Carlson", 120124 },
                { "Harper", 119868 },
                { "George", 119778 },
                { "Greene", 119604 },
                { "Burke", 119175 },
                { "Guzman", 118390 },
                { "Morrison", 117939 },
                { "Munoz", 117774 },
                { "Jacobs", 115540 },
                { "Obrien", 115385 },
                { "Lawson", 115186 },
                { "Franklin", 114859 },
                { "Lynch", 114448 },
                { "Bishop", 114034 },
                { "Carr", 113892 },
                { "Salazar", 113468 },
                { "Austin", 113160 },
                { "Mendez", 112736 },
                { "Gilbert", 112406 },
                { "Jensen", 112332 },
                { "Williamson", 112216 },
                { "Montgomery", 112144 },
                { "Harvey", 112136 },
                { "Oliver", 111641 },
                { "Howell", 109634 },
                { "Dean", 109230 },
                { "Hanson", 109079 },
                { "Weber", 107866 },
                { "Garrett", 107777 },
                { "Sims", 107244 },
                { "Burton", 107158 },
                { "Fuller", 106682 },
                { "Soto", 106631 },
                { "Mccoy", 106481 },
                { "Welch", 105804 },
                { "Chen", 105544 },
                { "Schultz", 104962 },
                { "Walters", 104281 },
                { "Reid", 104007 },
                { "Fields", 103242 },
                { "Walsh", 103216 },
                { "Little", 102718 },
                { "Fowler", 102620 },
                { "Bowman", 102239 },
                { "Davidson", 102044 },
                { "May", 101726 },
                { "Day", 101676 },
                { "Schneider", 100553 },
                { "Newman", 100491 },
                { "Brewer", 100465 },
                { "Lucas", 100417 },
                { "Holland", 99885 },
                { "Wong", 99392 },
                { "Banks", 99294 },
                { "Santos", 98993 },
                { "Curtis", 98958 },
                { "Pearson", 98728 },
                { "Delgado", 98675 },
                { "Valdez", 98610 },
                { "Pena", 98345 },
                { "Rios", 96569 },
                { "Douglas", 96425 },
                { "Sandoval", 96303 },
                { "Barrett", 95896 },
                { "Hopkins", 94603 },
                { "Keller", 94300 },
                { "Guerrero", 94152 },
                { "Stanley", 93817 },
                { "Bates", 93743 },
                { "Alvarado", 93723 },
                { "Beck", 93161 },
                { "Ortega", 93131 },
                { "Wade", 92834 },
                { "Estrada", 92831 },
                { "Contreras", 92660 },
                { "Barnett", 92287 },
                { "Caldwell", 91338 },
                { "Santiago", 90967 },
                { "Lambert", 90618 },
                { "Powers", 90401 },
                { "Chambers", 90325 },
                { "Nunez", 90208 },
                { "Craig", 89591 },
                { "Leonard", 89198 },
                { "Lowe", 89178 },
                { "Rhodes", 88917 },
                { "Byrd", 88811 },
                { "Gregory", 88810 },
                { "Shelton", 88326 },
                { "Frazier", 88325 },
                { "Becker", 88114 },
                { "Maldonado", 88016 },
                { "Fleming", 87949 },
                { "Vega", 87728 },
                { "Sutton", 87373 },
                { "Cohen", 87226 },
                { "Jennings", 87038 },
                { "Parks", 86346 },
                { "Mcdaniel", 86317 },
                { "Watts", 86228 },
                { "Barker", 85221 },
                { "Norris", 85212 },
                { "Vaughn", 85037 },
                { "Vazquez", 84926 },
                { "Holt", 84710 },
                { "Schwartz", 84699 },
                { "Steele", 84353 },
                { "Benson", 84233 },
                { "Neal", 83849 },
                { "Dominguez", 83731 },
                { "Horton", 83523 },
                { "Terry", 83437 },
                { "Wolfe", 83112 },
                { "Hale", 82955 },
                { "Lyons", 82258 },
                { "Graves", 82179 },
                { "Haynes", 82037 },
                { "Miles", 81933 },
                { "Park", 81890 },
                { "Warner", 81824 },
                { "Padilla", 81805 },
                { "Bush", 81524 },
                { "Thornton", 81191 },
                { "Mccarthy", 81035 },
                { "Mann", 81022 },
                { "Zimmerman", 80944 },
                { "Erickson", 80936 },
                { "Fletcher", 80932 },
                { "Mckinney", 80616 },
                { "Page", 80493 },
                { "Dawson", 80190 },
                { "Joseph", 80030 },
                { "Marquez", 79951 },
                { "Reeves", 79817 },
                { "Klein", 79685 },
                { "Espinoza", 79322 },
                { "Baldwin", 79151 },
                { "Moran", 78546 },
                { "Love", 78323 },
                { "Robbins", 78141 },
                { "Higgins", 78107 },
                { "Ball", 77561 },
                { "Cortez", 77492 },
                { "Le", 77453 },
                { "Griffith", 77429 },
                { "Bowen", 77078 },
                { "Sharp", 76868 },
                { "Cummings", 76707 },
                { "Ramsey", 76625 },
                { "Hardy", 76608 },
                { "Swanson", 76539 },
                { "Barber", 76504 },
                { "Acosta", 76477 },
                { "Luna", 76127 },
                { "Chandler", 76114 },
                { "Blair", 75135 },
                { "Daniel", 75135 },
                { "Cross", 75134 },
                { "Simon", 74839 },
                { "Dennis", 74784 },
                { "Oconnor", 74756 },
                { "Quinn", 74531 },
                { "Gross", 74285 },
                { "Navarro", 73970 },
                { "Moss", 73750 },
                { "Fitzgerald", 73522 },
                { "Doyle", 73518 },
                { "Mclaughlin", 73128 },
                { "Rojas", 73071 },
                { "Rodgers", 73021 },
                { "Stevenson", 72892 },
                { "Singh", 72642 },
                { "Yang", 72627 },
                { "Figueroa", 72533 },
                { "Harmon", 72414 },
                { "Newton", 72328 },
                { "Paul", 72248 },
                { "Manning", 72069 },
                { "Garner", 72052 },
                { "Mcgee", 71925 },
                { "Reese", 71754 },
                { "Francis", 71723 },
                { "Burgess", 71604 },
                { "Adkins", 71528 },
                { "Goodman", 71482 },
                { "Curry", 71344 },
                { "Brady", 71175 },
                { "Christensen", 71144 },
                { "Potter", 71103 },
                { "Walton", 70997 },
                { "Goodwin", 70333 },
                { "Mullins", 70286 },
                { "Molina", 70211 },
                { "Webster", 70123 },
                { "Fischer", 70095 },
                { "Campos", 69950 },
                { "Avila", 69843 },
                { "Sherman", 69840 },
                { "Todd", 69810 },
                { "Chang", 69756 },
                { "Blake", 69279 },
                { "Malone", 69257 },
                { "Wolf", 68905 },
                { "Hodges", 68868 },
                { "Juarez", 68785 },
                { "Gill", 68699 },
                { "Farmer", 68309 },
                { "Hines", 68145 },
                { "Gallagher", 68075 },
                { "Duran", 68046 },
                { "Hubbard", 68021 },
                { "Cannon", 67923 },
                { "Miranda", 67646 },
                { "Wang", 67570 },
                { "Saunders", 67210 },
                { "Tate", 67208 },
                { "Mack", 67154 },
                { "Hammond", 67063 },
                { "Carrillo", 67054 },
                { "Townsend", 66853 },
                { "Wise", 66738 },
                { "Ingram", 66665 },
                { "Barton", 66622 },
                { "Mejia", 66534 },
                { "Ayala", 66515 },
                { "Schroeder", 66412 },
                { "Hampton", 66378 },
                { "Rowe", 66205 },
                { "Parsons", 66203 },
                { "Frank", 65918 },
                { "Waters", 65817 },
                { "Strickland", 65814 },
                { "Osborne", 65802 },
                { "Maxwell", 65779 },
                { "Chan", 65719 },
                { "Deleon", 65598 },
                { "Norman", 65269 },
                { "Harrington", 65131 },
                { "Casey", 64815 },
                { "Patton", 64772 },
                { "Logan", 64576 },
                { "Bowers", 64496 },
                { "Mueller", 64305 },
                { "Glover", 64180 },
                { "Floyd", 64141 },
                { "Hartman", 63827 },
                { "Buchanan", 63825 },
                { "Cobb", 63739 },
                { "French", 63149 },
                { "Kramer", 63023 },
                { "Mccormick", 62663 },
                { "Clarke", 62546 },
                { "Tyler", 62534 },
                { "Gibbs", 62514 },
                { "Moody", 62344 },
                { "Conner", 62335 },
                { "Sparks", 62234 },
                { "Mcguire", 62116 },
                { "Leon", 62034 },
                { "Bauer", 61979 },
                { "Norton", 61805 },
                { "Pope", 61750 },
                { "Flynn", 61747 },
                { "Hogan", 61651 },
                { "Robles", 61619 },
                { "Salinas", 61582 },
                { "Yates", 61400 },
                { "Lindsey", 61199 },
                { "Lloyd", 61154 },
                { "Marsh", 60999 },
                { "Mcbride", 60874 },
                { "Owen", 60461 },
                { "Solis", 60045 },
                { "Pham", 59949 },
                { "Lang", 59843 },
                { "Pratt", 59801 },
                { "Lara", 59731 },
                { "Brock", 59682 },
                { "Ballard", 59660 },
                { "Trujillo", 59609 },
                { "Shaffer", 59227 },
                { "Drake", 59055 },
                { "Roman", 59020 },
                { "Aguirre", 58918 },
                { "Morton", 58788 },
                { "Stokes", 58687 },
                { "Lamb", 58555 },
                { "Pacheco", 58534 },
                { "Patrick", 58257 },
                { "Cochran", 58233 },
                { "Shepherd", 57935 },
                { "Cain", 57873 },
                { "Burnett", 57859 },
                { "Hess", 57822 },
                { "Li", 57786 },
                { "Cervantes", 57685 },
                { "Olsen", 57357 },
                { "Briggs", 57297 },
                { "Ochoa", 57210 },
                { "Cabrera", 57171 },
                { "Velasquez", 57163 },
                { "Montoya", 57075 },
                { "Roth", 57030 },
                { "Meyers", 56744 },
                { "Cardenas", 56618 },
                { "Fuentes", 56441 },
                { "Weiss", 56153 },
                { "Hoover", 56068 },
                { "Wilkins", 56068 },
                { "Nicholson", 55986 },
                { "Underwood", 55973 },
                { "Short", 55903 },
                { "Carson", 55821 },
                { "Morrow", 55664 },
                { "Colon", 55512 },
                { "Holloway", 55466 },
                { "Summers", 55391 },
                { "Bryan", 55269 },
                { "Petersen", 55185 },
                { "Mckenzie", 55084 },
                { "Serrano", 55057 },
                { "Wilcox", 54987 },
                { "Carey", 54924 },
                { "Clayton", 54875 },
                { "Poole", 54706 },
                { "Calderon", 54691 },
                { "Gallegos", 54672 },
                { "Greer", 54611 },
                { "Rivas", 54588 },
                { "Guerra", 54575 },
                { "Decker", 54450 },
                { "Collier", 54414 },
                { "Wall", 54401 },
                { "Whitaker", 54343 },
                { "Bass", 54296 },
                { "Flowers", 54277 },
                { "Davenport", 54206 },
                { "Conley", 54194 },
                { "Houston", 54026 },
                { "Huff", 53892 },
                { "Copeland", 53771 },
                { "Hood", 53737 },
                { "Monroe", 53475 },
                { "Massey", 53459 },
                { "Roberson", 53198 },
                { "Combs", 53180 },
                { "Franco", 53161 },
                { "Larsen", 52963 },
                { "Pittman", 52689 },
                { "Randall", 52495 },
                { "Skinner", 52490 },
                { "Wilkinson", 52483 },
                { "Kirby", 52473 },
                { "Cameron", 52439 },
                { "Bridges", 52260 },
                { "Anthony", 52146 },
                { "Richard", 52138 },
                { "Kirk", 52056 },
                { "Bruce", 52004 },
                { "Singleton", 51797 },
                { "Mathis", 51796 },
                { "Bradford", 51726 },
                { "Boone", 51679 },
                { "Abbott", 51620 },
                { "Charles", 51518 },
                { "Allison", 51504 },
                { "Sweeney", 51500 },
                { "Atkinson", 51489 },
                { "Horn", 51380 },
                { "Jefferson", 51361 },
                { "Rosales", 51336 },
                { "York", 51334 },
                { "Christian", 51177 },
                { "Phelps", 51154 },
                { "Farrell", 51095 },
                { "Castaneda", 51089 },
                { "Nash", 51021 },
                { "Dickerson", 51017 },
                { "Bond", 50980 },
                { "Wyatt", 50874 },
                { "Foley", 50852 },
                { "Chase", 50777 },
                { "Gates", 50748 },
                { "Vincent", 50628 },
                { "Mathews", 50608 },
                { "Hodge", 50577 },
                { "Garrison", 50482 },
                { "Trevino", 50454 },
                { "Villarreal", 50351 },
                { "Heath", 50307 },
                { "Dalton", 50166 },
                { "Valencia", 50026 },
                { "Callahan", 49925 },
                { "Hensley", 49858 },
                { "Atkins", 49754 },
                { "Huffman", 49737 },
                { "Roy", 49725 },
                { "Boyer", 49601 },
                { "Shields", 49556 },
                { "Lin", 49360 },
                { "Hancock", 49330 },
                { "Grimes", 49245 },
                { "Glenn", 49241 },
                { "Cline", 49167 },
                { "Delacruz", 49158 },
                { "Camacho", 49000 },
                { "Dillon", 48833 },
                { "Parrish", 48823 },
                { "Oneill", 48656 },
                { "Melton", 48594 },
                { "Booth", 48580 },
                { "Kane", 48527 },
                { "Berg", 48480 },
                { "Harrell", 48471 },
                { "Pitts", 48462 },
                { "Savage", 48367 },
                { "Wiggins", 48355 },
                { "Brennan", 48296 },
                { "Salas", 48282 },
                { "Marks", 48281 },
                { "Russo", 48126 },
                { "Sawyer", 47979 },
                { "Baxter", 47857 },
                { "Golden", 47839 },
                { "Hutchinson", 47809 },
                { "Liu", 47665 },
                { "Walter", 47615 },
                { "Mcdowell", 47526 },
                { "Wiley", 47503 },
                { "Rich", 47477 },
                { "Humphrey", 47470 },
                { "Johns", 47289 },
                { "Koch", 47286 },
                { "Suarez", 47235 },
                { "Hobbs", 47220 },
                { "Beard", 47128 },
                { "Gilmore", 47050 },
                { "Ibarra", 46895 },
                { "Keith", 46747 },
                { "Macias", 46739 },
                { "Khan", 46713 },
                { "Andrade", 46702 },
                { "Ware", 46682 },
                { "Stephenson", 46662 },
                { "Henson", 46609 },
                { "Wilkerson", 46605 },
                { "Dyer", 46574 },
                { "Mcclure", 46505 },
                { "Blackwell", 46495 },
                { "Mercado", 46437 },
                { "Tanner", 46412 },
                { "Eaton", 46403 },
                { "Clay", 46264 },
                { "Barron", 46196 },
                { "Beasley", 46179 },
                { "Oneal", 46161 },
                { "Preston", 45850 },
                { "Small", 45850 },
                { "Wu", 45815 },
                { "Zamora", 45806 },
                { "Macdonald", 45782 },
                { "Vance", 45763 },
                { "Snow", 45689 },
                { "Mcclain", 45560 },
                { "Stafford", 45349 },
                { "Orozco", 45289 },
                { "Barry", 45044 },
                { "English", 45032 },
                { "Shannon", 44902 },
                { "Kline", 44900 },
                { "Jacobson", 44874 },
                { "Woodard", 44830 },
                { "Huang", 44715 },
                { "Kemp", 44701 },
                { "Mosley", 44698 },
                { "Prince", 44640 },
                { "Merritt", 44626 },
                { "Hurst", 44587 },
                { "Villanueva", 44570 },
                { "Roach", 44454 },
                { "Nolan", 44421 },
                { "Lam", 44385 },
                { "Yoder", 44245 },
                { "Mccullough", 44123 },
                { "Lester", 43919 },
                { "Santana", 43875 },
                { "Valenzuela", 43770 },
                { "Winters", 43762 },
                { "Barrera", 43720 },
                { "Leach", 43666 },
                { "Orr", 43666 },
                { "Berger", 43556 },
                { "Mckee", 43555 },
                { "Strong", 43430 },
                { "Conway", 43395 },
                { "Stein", 43331 },
                { "Whitehead", 43310 },
                { "Bullock", 43021 },
                { "Escobar", 42955 },
                { "Knox", 42937 },
                { "Meadows", 42884 },
                { "Solomon", 42839 },
                { "Velez", 42820 },
                { "Odonnell", 42802 },
                { "Kerr", 42758 },
                { "Stout", 42669 },
                { "Blankenship", 42663 },
                { "Browning", 42642 },
                { "Kent", 42597 },
                { "Lozano", 42567 },
                { "Bartlett", 42512 },
                { "Pruitt", 42463 },
                { "Buck", 42441 },
                { "Barr", 42432 },
                { "Gaines", 42369 },
                { "Durham", 42365 },
                { "Gentry", 42357 },
                { "Mcintyre", 42335 },
                { "Sloan", 42281 },
                { "Melendez", 42139 },
                { "Rocha", 42139 },
                { "Herman", 42091 },
                { "Sexton", 42080 },
                { "Moon", 42062 },
                { "Hendricks", 41879 },
                { "Rangel", 41868 },
                { "Stark", 41863 },
                { "Lowery", 41670 },
                { "Hardin", 41664 },
                { "Hull", 41656 },
                { "Sellers", 41561 },
                { "Ellison", 41459 },
                { "Calhoun", 41452 },
                { "Gillespie", 41393 },
                { "Mora", 41348 },
                { "Knapp", 41267 },
                { "Mccall", 41231 },
                { "Morse", 41112 },
                { "Dorsey", 41104 },
                { "Weeks", 41053 },
                { "Nielsen", 41007 },
                { "Livingston", 40964 },
                { "Leblanc", 40923 },
                { "Mclean", 40871 },
                { "Bradshaw", 40794 },
                { "Glass", 40724 },
                { "Middleton", 40708 },
                { "Buckley", 40706 },
                { "Schaefer", 40663 },
                { "Frost", 40582 },
                { "Howe", 40555 },
                { "House", 40477 },
                { "Mcintosh", 40453 },
                { "Ho", 40413 },
                { "Pennington", 40339 },
                { "Reilly", 40310 },
                { "Hebert", 40283 },
                { "Mcfarland", 40244 },
                { "Hickman", 40224 },
                { "Noble", 40217 },
                { "Spears", 40203 },
                { "Conrad", 40102 },
                { "Arias", 40086 },
                { "Galvan", 40046 },
                { "Velazquez", 40030 },
                { "Huynh", 40011 },
                { "Frederick", 39909 },
                { "Randolph", 39742 },
                { "Cantu", 39601 },
                { "Fitzpatrick", 39501 },
                { "Mahoney", 39440 },
                { "Peck", 39432 },
                { "Villa", 39402 },
                { "Michael", 39369 },
                { "Donovan", 39270 },
                { "Mcconnell", 39203 },
                { "Walls", 39166 },
                { "Boyle", 39141 },
                { "Mayer", 39111 },
                { "Zuniga", 39057 },
                { "Giles", 39002 },
                { "Pineda", 38999 },
                { "Pace", 38975 },
                { "Hurley", 38971 },
                { "Mays", 38914 },
                { "Mcmillan", 38896 },
                { "Crosby", 38844 },
                { "Ayers", 38836 },
                { "Case", 38759 },
                { "Bentley", 38714 },
                { "Shepard", 38705 },
                { "Everett", 38702 },
                { "Pugh", 38691 },
                { "David", 38659 },
                { "Mcmahon", 38557 },
                { "Dunlap", 38516 },
                { "Bender", 38464 },
                { "Hahn", 38354 },
                { "Harding", 38340 },
                { "Acevedo", 38232 },
                { "Raymond", 38158 },
                { "Blackburn", 38137 },
                { "Duffy", 37962 },
                { "Landry", 37961 },
                { "Dougherty", 37903 },
                { "Bautista", 37847 },
                { "Shah", 37833 },
                { "Potts", 37687 },
                { "Arroyo", 37678 },
                { "Valentine", 37669 },
                { "Meza", 37662 },
                { "Gould", 37660 },
                { "Vaughan", 37591 },
                { "Fry", 37542 },
                { "Rush", 37470 },
                { "Avery", 37440 },
                { "Herring", 37353 },
                { "Dodson", 37298 },
                { "Clements", 37237 },
                { "Sampson", 37234 },
                { "Tapia", 37201 },
                { "Bean", 37145 },
                { "Lynn", 37125 },
                { "Crane", 37123 },
                { "Farley", 37116 },
                { "Cisneros", 37050 },
                { "Benton", 37032 },
                { "Ashley", 37021 },
                { "Mckay", 36948 },
                { "Finley", 36874 },
                { "Best", 36862 },
                { "Blevins", 36841 },
                { "Friedman", 36833 },
                { "Moses", 36814 },
                { "Sosa", 36813 },
                { "Blanchard", 36764 },
                { "Huber", 36729 },
                { "Frye", 36716 },
                { "Krueger", 36694 },
                { "Bernard", 36546 },
                { "Rosario", 36539 },
                { "Rubio", 36531 },
                { "Mullen", 36442 },
                { "Benjamin", 36439 },
                { "Haley", 36433 },
                { "Chung", 36422 },
                { "Moyer", 36421 },
                { "Choi", 36390 },
                { "Horne", 36288 },
                { "Yu", 36285 },
                { "Woodward", 36242 },
                { "Ali", 36079 },
                { "Nixon", 36037 },
                { "Hayden", 36024 },
                { "Rivers", 35980 },
                { "Estes", 35839 },
                { "Mccarty", 35718 },
                { "Richmond", 35715 },
                { "Stuart", 35701 },
                { "Maynard", 35648 },
                { "Brandt", 35616 },
                { "Oconnell", 35610 },
                { "Hanna", 35599 },
                { "Sanford", 35565 },
                { "Sheppard", 35554 },
                { "Church", 35539 },
                { "Burch", 35521 },
                { "Levy", 35464 },
                { "Rasmussen", 35453 },
                { "Coffey", 35442 },
                { "Ponce", 35400 },
                { "Faulkner", 35389 },
                { "Donaldson", 35387 },
                { "Schmitt", 35326 },
                { "Novak", 35282 },
                { "Costa", 35227 },
                { "Montes", 35196 },
                { "Booker", 35101 },
                { "Cordova", 35074 },
                { "Waller", 35001 },
                { "Arellano", 34999 },
                { "Maddox", 34970 },
                { "Mata", 34888 },
                { "Bonilla", 34824 },
                { "Stanton", 34812 },
                { "Compton", 34788 },
                { "Kaufman", 34786 },
                { "Dudley", 34770 },
                { "Mcpherson", 34763 },
                { "Beltran", 34736 },
                { "Dickson", 34698 },
                { "Mccann", 34692 },
                { "Villegas", 34684 },
                { "Proctor", 34682 },
                { "Hester", 34675 },
                { "Cantrell", 34674 },
                { "Daugherty", 34650 },
                { "Cherry", 34615 },
                { "Bray", 34575 },
                { "Davila", 34541 },
                { "Rowland", 34498 },
                { "Levine", 34472 },
                { "Madden", 34472 },
                { "Spence", 34435 },
                { "Good", 34430 },
                { "Irwin", 34374 },
                { "Werner", 34352 },
                { "Krause", 34345 },
                { "Petty", 34278 },
                { "Whitney", 34251 },
                { "Baird", 34233 },
                { "Hooper", 34084 },
                { "Pollard", 34079 },
                { "Zavala", 34068 },
                { "Jarvis", 34050 },
                { "Holden", 34041 },
                { "Haas", 34032 },
                { "Hendrix", 34032 },
                { "Mcgrath", 34031 },
                { "Bird", 33962 },
                { "Lucero", 33922 },
                { "Terrell", 33914 },
                { "Riggs", 33868 },
                { "Joyce", 33843 },
                { "Mercer", 33797 },
                { "Rollins", 33797 },
                { "Galloway", 33773 },
                { "Duke", 33745 },
                { "Odom", 33717 },
                { "Andersen", 33508 },
                { "Downs", 33494 },
                { "Hatfield", 33464 },
                { "Benitez", 33441 },
                { "Archer", 33411 },
                { "Huerta", 33348 },
                { "Travis", 33339 },
                { "Mcneil", 33239 },
                { "Hinton", 33209 },
                { "Zhang", 33202 },
                { "Hays", 33194 },
                { "Mayo", 33126 },
                { "Fritz", 33068 },
                { "Branch", 33040 },
                { "Mooney", 32953 },
                { "Ewing", 32925 },
                { "Ritter", 32864 },
                { "Esparza", 32772 },
                { "Frey", 32735 },
                { "Braun", 32676 },
                { "Gay", 32672 },
                { "Riddle", 32654 },
                { "Haney", 32644 },
                { "Kaiser", 32567 },
                { "Holder", 32466 },
                { "Chaney", 32433 },
                { "Mcknight", 32386 },
                { "Gamble", 32377 },
                { "Vang", 32333 },
                { "Cooley", 32287 },
                { "Carney", 32282 },
                { "Cowan", 32242 },
                { "Forbes", 32228 },
                { "Ferrell", 32174 },
                { "Davies", 32165 },
                { "Barajas", 32147 },
                { "Shea", 32069 },
                { "Osborn", 32044 },
                { "Bright", 32042 },
                { "Cuevas", 32015 },
                { "Bolton", 31995 },
                { "Murillo", 31964 },
                { "Lutz", 31940 },
                { "Duarte", 31896 },
                { "Kidd", 31886 },
                { "Key", 31882 },
                { "Cooke", 31860 }
            };
        }
    }
}