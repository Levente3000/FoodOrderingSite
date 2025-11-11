using Microsoft.Playwright;

namespace ModelTest;

public class ModelTestsSetup : PageTest
{
    private const string ValidUser = "test";
    private const string ValidPassword = "test";
    private const string PromoCode = "promo";

    protected const string AppBaseUrl = "http://localhost:4200";

    protected Dictionary<string, Func<Task>> inputs = null!;
    protected Dictionary<string, Func<Task>> outputs = null!;
    protected EfsmContext context;

    [SetUp]
    public void SetupDictionaries()
    {
        context = new EfsmContext();
        context.Init();

        inputs = new Dictionary<string, Func<Task>>
        {
            ["loginWithIncorrectCredentials"] = () => LoginAsync("wrong", "wrong"),
            ["loginWithCorrectCredentials"] = () => LoginAsync(ValidUser, ValidPassword),

            ["clickLogOut"] = LogoutAsync,
            ["clickCartNav"] = ClickCartNavAsync,
            ["clickProfilePageNav"] = ClickProfilePageNavAsync,
            ["clickRestaurantListNav"] = ClickRestaurantListNavAsync,
            ["clickHomeNav"] = ClickHomeNavAsync,
            ["clickHomeNavFromLogin"] = ClickHomeNavAsync,

            ["clickARestaurant"] = ClickARestaurantAsync,
            ["clickProduct"] = ClickProductAsync,
            ["clickAddToFavourites"] = ClickAddToFavouritesAsync,

            ["clickAddProduct"] = ClickAddProductAsync,
            ["clickRemoveProduct"] = ClickRemoveProductAsync,
            ["clickClearCart"] = ClickClearCartAsync,
            ["clickPlaceOrder"] = ClickPlaceOrderAsync,

            ["addPromoCode"] = AddPromoCodeAsync,
            ["removePromoCode"] = RemovePromoCodeAsync,

            ["clickCancel"] = ClickCancelProductWindowAsync,
            ["clickUpdateProfileToCart"] = ClickUpdateProfileToCartAsync,
            ["filterRestaurants"] = FilterRestaurantsAsync,
        };

        outputs = new Dictionary<string, Func<Task>>
        {
            ["loginPageFromLogin"] = AssertLoginPageAsync,
            ["homePageFromLogin"] = AssertHomePageAsync,
            ["loginPage"] = AssertLoginPageAsync,
            ["homePage"] = AssertHomePageAsync,
            ["cartPage"] = AssertCartPageAsync,
            ["profilePage"] = AssertProfilePageAsync,
            ["restaurantListPage"] = AssertRestaurantListPageAsync,
            ["restaurantDetailPage"] = AssertRestaurantDetailPageAsync,
            ["productDetailsWindow"] = AssertProductDetailsWindowAsync,
        };
    }

    private async Task LoginAsync(string username, string password)
    {
        await Page.GotoAsync(AppBaseUrl);
        await Page.GetByLabel("Username or email").FillAsync(username);
        await Page.GetByLabel("Password").FillAsync(password);
        await Page.Locator("#kc-login").ClickAsync();
    }

    private async Task LogoutAsync()
    {
        await Context.ClearCookiesAsync();

        await Page.GotoAsync(AppBaseUrl, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });
    }


    private Task ClickCartNavAsync()
    {
        return Page.GetByText("shopping_cart").ClickAsync();
    }

    private Task ClickProfilePageNavAsync()
    {
        return Page.Locator(".mat-icon").GetByText("person").ClickAsync();
    }

    private Task ClickRestaurantListNavAsync()
    {
        return Page.Locator("a[routerlink='/restaurants']").ClickAsync();
    }

    private Task ClickHomeNavAsync()
    {
        return Page.Locator("a[routerlink='/home']").ClickAsync();
    }

    private Task ClickARestaurantAsync()
    {
        return Page.GetByText("Blue Parrot").First.ClickAsync();
    }

    private Task ClickProductAsync()
    {
        return Page.Locator(".product-name").GetByText("Lasagne").ClickAsync();
    }

    private Task ClickAddToFavouritesAsync()
    {
        return Page.Locator(".product-name").GetByText("Lasagne").HoverAsync();
    }

    private async Task ClickAddProductAsync()
    {
        context.ApplyTransition("clickAddProduct");
        await Page.Locator(".product-dialog .submit-button").GetByText("Add to cart").Last.ClickAsync();
    }

    private async Task ClickRemoveProductAsync()
    {
        context.ApplyTransition("clickRemoveProduct");
        await Page.Locator(".product-remove").GetByText("Remove").First.ClickAsync();
        await AssertCartCountMatchesModelAsync();
    }

    private async Task ClickClearCartAsync()
    {
        context.ApplyTransition("clickClearCart");
        await Page.Locator(".clear-cart").GetByText("Clear cart").ClickAsync();
        await AssertCartCountMatchesModelAsync();
    }

    private async Task ClickPlaceOrderAsync()
    {
        context.ApplyTransition("clickPlaceOrder");
        await Page.Locator(".place-order").GetByText("Place order").ClickAsync();
    }

    private async Task AddPromoCodeAsync()
    {
        context.ApplyTransition("addPromoCode");
        await Page.GetByPlaceholder("Write the promo code here...").FillAsync(PromoCode);
        await Page.Locator(".promo-code-button").GetByText("Apply").ClickAsync();
        await AssertPromoStateMatchesModelAsync();
    }

    private async Task RemovePromoCodeAsync()
    {
        context.ApplyTransition("removePromoCode");
        await Page.Locator(".clear-promo-code").GetByText("Clear code").ClickAsync();
        await AssertPromoStateMatchesModelAsync();
    }

    private async Task ClickCancelProductWindowAsync()
    {
        await Page.Keyboard.PressAsync("Escape");
    }

    private async Task ClickUpdateProfileToCartAsync()
    {
        await Page.GetByPlaceholder("Enter your address").FillAsync("Budapest, Test utca 2");
        await Page.GetByText("Update profile").ClickAsync();
    }

    private async Task FilterRestaurantsAsync()
    {
        await Page.GetByPlaceholder("Type to filter...").FillAsync("");
        var redDragon = Page.GetByText("Red Dragon");
        await Expect(redDragon).ToBeVisibleAsync();

        await Page.GetByPlaceholder("Type to filter...").FillAsync("blue");

        await Expect(redDragon).ToHaveCountAsync(0);
    }

    private async Task AssertLoginPageAsync()
    {
        await Expect(Page.GetByText("Sign in to your account")).ToBeVisibleAsync();
        await Expect(Page.GetByLabel("Username or email")).ToBeVisibleAsync();
    }

    private async Task AssertHomePageAsync()
    {
        await Expect(Page).ToHaveURLAsync(new Regex("localhost:4200"));
        await Expect(Page.GetByText("Create Restaurant")).ToBeVisibleAsync();
    }

    private async Task AssertCartPageAsync()
    {
        await Expect(Page.GetByText("My shopping cart")).ToBeVisibleAsync();

        await AssertCartCountMatchesModelAsync();
    }

    private async Task AssertProfilePageAsync()
    {
        await Expect(Page.GetByText("Update profile")).ToBeVisibleAsync();
    }

    private async Task AssertRestaurantListPageAsync()
    {
        await Expect(Page.Locator("button.filter-button")).ToBeVisibleAsync();
    }

    private async Task AssertRestaurantDetailPageAsync()
    {
        await Expect(Page.Locator(".more-information").GetByText("More information")).ToBeVisibleAsync();
    }

    private async Task AssertProductDetailsWindowAsync()
    {
        await Expect(Page.Locator(".product-actions .submit-button").Last).ToBeVisibleAsync();
    }

    private async Task AssertCartCountMatchesModelAsync()
    {
        var headerItem = Page.Locator("app-shopping-cart .shopping-cart-header .header-item").First;
        var expectedNumber = context.ProductInCart;
        await Expect(headerItem).ToHaveTextAsync($"{expectedNumber} item");
    }

    private async Task AssertPromoStateMatchesModelAsync()
    {
        var promoLabel = Page.Locator(".applied-promo .applied-promo-code").GetByText(PromoCode);

        if (context.IsPromoCodeActivated)
        {
            await Expect(promoLabel).ToBeVisibleAsync();
            await Expect(promoLabel).ToHaveCountAsync(1);
        }
        else
        {
            await Expect(promoLabel).ToHaveCountAsync(0);
        }
    }
}