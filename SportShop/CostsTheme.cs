using MudBlazor;

namespace SportShop
{
    public class CostsTheme : MudTheme
    {
        public CostsTheme()
        {
            Palette = new Palette()
            {
                //Primary = Colors.Blue.Default,
                //Secondary = Colors.Green.Accent4,
                //AppbarBackground = Colors.Red.Default,
                //Tertiary = "#7CB749",
                AppbarBackground= "#1EA3DB",
                DrawerBackground= "#9e9e9e",
                DrawerIcon = "#FDFDFD",
                DrawerText = "#FDFDFD",
                Primary = "#1EA3DB",
                Secondary = "#373987",
                Tertiary = "#0A3A5A",
                TextPrimary = "#4D4D4D",
                LinesDefault = "#a0a0a5",
                Warning = "#D68500",
            };

            LayoutProperties = new LayoutProperties()
            {
                DefaultBorderRadius = "8px",
                DrawerWidthLeft ="350px",
                DrawerMiniWidthLeft = "62px"
            };

            Typography = new Typography()
            {
                Default = new Default()
                {
                    FontFamily = new[] { "Gotham Book" },
                    FontWeight = 400,
                    FontSize = ".875rem",
                    LineHeight = 1.43,
                    LetterSpacing = ".01071em"
                },
                H1 = new H1()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 300,
                    FontSize = "6rem",
                    LineHeight = 1.167,
                    LetterSpacing = "-.01562em"
                },
                H2 = new H2()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 300,
                    FontSize = "3.75rem",
                    LineHeight = 1.2,
                    LetterSpacing = "-.00833em"
                },
                H3 = new H3()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 400,
                    FontSize = "3rem",
                    LineHeight = 1.167,
                    LetterSpacing = "0"
                },
                H4 = new H4()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 400,
                    FontSize = "2.125rem",
                    LineHeight = 1.235,
                    LetterSpacing = ".00735em"
                },
                H5 = new H5()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 400,
                    FontSize = "1.5rem",
                    LineHeight = 1.334,
                    LetterSpacing = "0"
                },
                H6 = new H6()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 500,
                    FontSize = "1.25rem",
                    LineHeight = 1.6,
                    LetterSpacing = ".0075em"
                },
                Subtitle1 = new Subtitle1()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 400,
                    FontSize = "1rem",
                    LineHeight = 1.75,
                    LetterSpacing = ".00938em"
                },
                Subtitle2 = new Subtitle2()
                {
                    FontFamily = new[] { "Gotham Bold" },
                    FontWeight = 500,
                    FontSize = ".875rem",
                    LineHeight = 1.57,
                    LetterSpacing = ".00714em"
                },
                Body1 = new Body1()
                {
                    FontFamily = new[] { "Gotham Book" },
                    FontWeight = 400,
                    FontSize = "1rem",
                    LineHeight = 1.5,
                    LetterSpacing = ".00938em"
                },
                Body2 = new Body2()
                {
                    FontFamily = new[] { "Gotham Book" },
                    FontWeight = 400,
                    FontSize = ".875rem",
                    LineHeight = 1.43,
                    LetterSpacing = ".01071em"
                },
                Button = new Button()
                {
                    FontFamily = new[] { "Gotham Book" },
                    FontWeight = 500,
                    FontSize = ".875rem",
                    LineHeight = 1.75,
                    LetterSpacing = ".02857em"
                },
                Caption = new Caption()
                {
                    FontFamily = new[] { "Gotham Book" },
                    FontWeight = 400,
                    FontSize = ".75rem",
                    LineHeight = 1.66,
                    LetterSpacing = ".03333em"
                },
                Overline = new Overline()
                {
                    FontFamily = new[] { "Gotham Book" },
                    FontWeight = 400,
                    FontSize = ".75rem",
                    LineHeight = 2.66,
                    LetterSpacing = ".08333em"
                }
            };

            Shadows = new Shadow();

            ZIndex = new ZIndex();
        }
    }
}