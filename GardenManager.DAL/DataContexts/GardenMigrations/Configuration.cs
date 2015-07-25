namespace GardenManager.DAL.DataContexts.GardenMigrations
{
    using GardenManager.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GardenManager.DAL.DataContexts.GardenContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\GardenMigrations";
        }

        protected override void Seed(GardenManager.DAL.DataContexts.GardenContext context)
        {
            context.SeedFamilies.AddOrUpdate(p => p.Name,
                new SeedFamily
                {
                    Name = "Umbellifers",
                    MoreInfo = "Umbellifers are a plant family whose flowers form in umbels – " +
                    "one of its fundamental characteristics and from which its name is derived. " +
                    "Some species like hemlock may be toxic whereas others are edible.",
                    Examples = "Dill, aniseed, angelica, carrot, caraway, celery, celeriac, " +
                    "chervil, coriander, cumin, fennel, parsnip, parsley"
                },
                new SeedFamily
                {
                    Name = "Labiates",
                    MoreInfo = "Labiates are a family of flowering plants whose leaves contain " +
                    "numerous small glands which secrete essential oils that make these plants " +
                    "very aromatic and thus used in herbal teas (mint, lemon balm), confectionery " +
                    "(mint), cooking (sage, thyme, savoury), perfumes (oregano, lavender), etc.",
                    Examples = "Basil, catnip, hyssop, lavender, marjoram, horehound, lemon balm, " +
                    "oregano, rosemary, savory, sage, thyme"
                },
                new SeedFamily
                {
                    Name = "Solanaceae",
                    MoreInfo = "Solanaceae are herbaceous plants, shrubs, trees or creepers from " +
                    "temperate to tropical regions.",
                    Examples = "Alkekengi, aubergine, pepper, potato, tobacco, tomato"
                },
                new SeedFamily
                {
                    Name = "Composites",
                    MoreInfo = "Composites represent a large plant family of nearly 13,000 species " +
                    "of primarily herbaceous plants but also included in the family are trees, " +
                    "shrubs and creepers.",
                    Examples = "Absinthe, artichoke, chamomile, cardoon, endive, tarragon, lettuce, " +
                    "dandelion, salsify"
                },
                new SeedFamily
                {
                    Name = "Crucifers",
                    MoreInfo = "Crucifers are characterised by a flower with four sepals, four petals " +
                    "in a cross shape, six stamens including two smaller ones, and by its silique fruit.",
                    Examples = "Cabbage, watercress, turnip, radish"
                },
                new SeedFamily
                {
                    Name = "Liliaceae",
                    MoreInfo = "Liliaceae are a very large family of plants whose leaves are generally " +
                    "vertical and very long with flowers with six coloured petals. These species can be " +
                    "used for ornamental, food, medicinal or textile purposes.",
                    Examples = "Garlic, asparagus, chives, shallot, onion, leek"
                },
                new SeedFamily
                {
                    Name = "Rosaceae",
                    MoreInfo = "Rosaceae are herbaceous or woody plants, with alternate, single or " +
                    "composite leaves, generally pink in colour.",
                    Examples = "Strawberry plant, cherry tree, raspberry cane, mulberry tree, pear tree, " +
                    "apple tree, plum tree, medlars"
                },
                new SeedFamily
                {
                    Name = "Cucurbits",
                    MoreInfo = "Cucurbits are herbaceous plants (very occasionally shrubs), more or less " +
                    "creeping or climbing – owing to spiral tendrils – from temperate, hot and tropical " +
                    "regions.",
                    Examples = "Pumpkin, squash, cucumber, melon"
                },
                new SeedFamily
                {
                    Name = "Chenopodiaceae",
                    MoreInfo = "Chenopodiaceae are plants without petals, often from ground with high salt " +
                    "or nitrate content.",
                    Examples = "Swiss chard, beetroot, spinach"
                },
                new SeedFamily
                {
                    Name = "Fabaceae",
                    MoreInfo = "Fabaceae, commonly called legumes, are generally herbaceous plants, " +
                    "shrubs, trees or creepers. It is a cosmopolitan family found in cold to tropical areas.",
                    Examples = "beans, peas, lentils, groundnuts, broad beans, soya"
                },
                new SeedFamily
                {
                    Name = "Poaceae",
                    MoreInfo = "Poaceae, formerly known as graminae, include nearly 12,000 species in more " +
                    "than 700 genera, and here we find most of the plant species commonly called \"grasses\", " +
                    "but also some other species such as bamboo for example.",
                    Examples = "Maize, rice, wheat, barley, oats, rye, millet"
                });

            context.Zones.AddOrUpdate(z => z.Zone,
                new PlantHardinessZone
                {
                    Zone = "1a",
                    TempRange = "-60, -55"
                },
                new PlantHardinessZone
                {
                    Zone = "1b",
                    TempRange = "-55, -50"
                },
                new PlantHardinessZone
                {
                    Zone = "2a",
                    TempRange = "-50, -45"
                },
                new PlantHardinessZone
                {
                    Zone = "2b",
                    TempRange = "-45, -40"
                },
                new PlantHardinessZone
                {
                    Zone = "3a",
                    TempRange = "-40, -35"
                },
                new PlantHardinessZone
                {
                    Zone = "3b",
                    TempRange = "-35, -30"
                },
                new PlantHardinessZone
                {
                    Zone = "4a",
                    TempRange = "-30, -25"
                },
                new PlantHardinessZone
                {
                    Zone = "4b",
                    TempRange = "-25, -20"
                },
                new PlantHardinessZone
                {
                    Zone = "5a",
                    TempRange = "-20, -15"
                },
                new PlantHardinessZone
                {
                    Zone = "5b",
                    TempRange = "-15, -10"
                },
                new PlantHardinessZone
                {
                    Zone = "6a",
                    TempRange = "-10, -5"
                },
                new PlantHardinessZone
                {
                    Zone = "6b",
                    TempRange = "-5, 0"
                },
                new PlantHardinessZone
                {
                    Zone = "7a",
                    TempRange = "0, 5"
                },
                new PlantHardinessZone
                {
                    Zone = "7b",
                    TempRange = "5, 10"
                },
                new PlantHardinessZone
                {
                    Zone = "8a",
                    TempRange = "10, 15"
                },
                new PlantHardinessZone
                {
                    Zone = "8b",
                    TempRange = "15, 20"
                },
                new PlantHardinessZone
                {
                    Zone = "9a",
                    TempRange = "20, 25"
                },
                new PlantHardinessZone
                {
                    Zone = "9b",
                    TempRange = "25, 30"
                },
                new PlantHardinessZone
                {
                    Zone = "10a",
                    TempRange = "30, 35"
                },
                new PlantHardinessZone
                {
                    Zone = "10b",
                    TempRange = "35, 40"
                },
                new PlantHardinessZone
                {
                    Zone = "11a",
                    TempRange = "40, 45"
                },
                new PlantHardinessZone
                {
                    Zone = "11b",
                    TempRange = "45, 50"
                },
                new PlantHardinessZone
                {
                    Zone = "12a",
                    TempRange = "50, 55"
                },
                new PlantHardinessZone
                {
                    Zone = "12b",
                    TempRange = "55, 60"
                },
                new PlantHardinessZone
                {
                    Zone = "13a",
                    TempRange = "60, 65"
                },
                new PlantHardinessZone
                {
                    Zone = "13b",
                    TempRange = "65, 70"
                });
        }
    }
}
