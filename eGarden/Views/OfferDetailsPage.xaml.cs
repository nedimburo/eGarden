namespace eGarden.Views;

public partial class OfferDetailsPage : ContentPage
{
	public OfferDetailsPage(string option)
	{
		InitializeComponent();
		PopulateDisplay(option);
	}

	private void PopulateDisplay(string option)
	{
		string imageUrl="";
		string title="";
		string description = "";
		string price= "";	
		if (option == "maint1")
		{
			imageUrl = "maintenance1.png";
			title = "The Basic Package";
			description = "The Basic Package entails comprehensive care for your garden, including lawn mowing, precise pruning of plants, and meticulous fertilization to encourage optimal growth and development throughout different seasons. This regular care ensures that your garden flourishes in its full beauty, creating a green oasis that captivates. Choose this package to provide your garden with everything it needs for a well-maintained appearance regardless of the season.";
			price = "25";

        }
        if (option == "maint2")
        {
            imageUrl = "maintenance2.png";
            title = "The Advanced Package";
            description = "The Advanced package represents care for your garden, encompassing all the benefits of the Basic package, while additionally providing protection for your plants against pests and thorough weed control. This comprehensive package also includes cleaning of all green and other surfaces, ensuring your garden receives complete care and preservation. Choose the Advanced package to guarantee an outstanding appearance for your garden with added protection and cleanliness throughout all seasons.";
            price = "45";

        }
        if (option == "maint3")
        {
            imageUrl = "maintenance3.png";
            title = "The Pro Package";
            description = "The Pro package is the ultimate choice that encompasses all the privileges from the Basic and Advanced packages, representing the pinnacle of our offerings. This exclusive package not only includes all aspects of garden care from the previous packages but also ensures the replacement of any deceased plants, providing a complete guarantee that your garden will radiate flawlessly throughout the entire year. Choose the Pro package to indulge in top-tier care and maintenance for your garden, creating an oasis of beauty that captivates throughout all seasons.";
            price = "75";

        }
		if (option == "deco1")
		{
			imageUrl = "decoration1.png";
			title = "The Rose Package";
			description = "The Rose Collection encompasses the cultivation of several rose varieties. Depending on your preferences, your garden could flourish with the most exquisite roses in various colors. The rose, a genus of woody plants from the Rosaceae family, is cultivated for its beautiful and fragrant flowers. To this day, numerous hybrids and cultivars of roses exist, each distinguished by the color and appearance of its blooms, fragrance, and the presence of thorns. Choose this package to have the queen of flowers grace your garden.";
			price = "120";
		}
        if (option == "deco2")
        {
            imageUrl = "decoration2.png";
            title = "The Small Trees Package";
            description = "The Japanese Maple is an ornamental, deciduous small tree or shrub that deserves a prominent place in any garden. It comes in various sizes, shapes, and leaf colors, ranging from shades of green to orange, red, and purple. The striking variability of the species, coupled with years of horticultural work, has resulted in an immense diversity of sizes and a wide array of leaf shapes and colors. Most Japanese Maple varieties require minimal maintenance and are easy to cultivate. Choose this package if you wish to indulge in the beautiful hues of this deciduous tree that demands little attention but yields an abundance of beauty.";
            price = "450";
        }
        if (option == "deco3")
        {
            imageUrl = "decoration3.png";
            title = "The Evergreen Plants Package";
            description = "In gardening, evergreen ornamental shrubs are used to permanently greenify parts of the garden, creating borders, hedges, or other visual barriers from shrubbery. If you have ample space for evergreen ornamental shrubs, consider yourself fortunate. However, even a small garden can be adorned with evergreen plants by opting for dwarf trees or low-growing evergreen shrubs. In gardening, these are valued for their flowering, pleasant fragrances, and attractive forms. Examples of such shrubs include boxwood, heather, cotoneaster, dwarf pine, large-flowered magnolia, privet, eucalyptus, laurel, and more. Choose the Evergreen Package and bestow a touch of greenery upon your garden.";
            price = "250";
        }
        if (option == "deco4")
        {
            imageUrl = "decoration4.png";
            title = "The Mix Package";
            description = "Flowers, the adornment of every garden and balcony. Flowers are not merely a garden ornament; they play a crucial role in achieving biological balance in the garden. Some flowers promote the growth of specific vegetables and act as effective protection against pests. The sight of diverse and vibrant colors awakens our senses and has a stimulating effect on our mood. Annuals, biennials, and perennials continually surprise and delight us with the diversity of their blooms, shapes, and hues. However, it's essential to plant the right flower in the right place to ensure it thrives beautifully and abundantly with minimal intervention. Choose this package and add a splash of color to the world.";
            price = "850";
        }
        Image.Source = imageUrl;
		Title.Text = title;
		Description.Text = description;
		Price.Text = price;
    }
}