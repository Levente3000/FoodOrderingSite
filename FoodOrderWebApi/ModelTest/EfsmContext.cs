namespace ModelTest;

public class EfsmContext
{
    public int ProductInCart { get; private set; }
    public bool IsPromoCodeActivated { get; private set; }

    public void Init()
    {
        ProductInCart = 0;
        IsPromoCodeActivated = false;
    }

    public void ApplyTransition(string input)
    {
        switch (input)
        {
            case "clickAddProduct":
                if (ProductInCart >= 1)
                    throw new InvalidOperationException("EFSM guard violated: productInCart < 1",
                        new Exception($"{ProductInCart}"));
                ProductInCart++;
                break;

            case "clickRemoveProduct":
                if (ProductInCart <= 0)
                    throw new InvalidOperationException("EFSM guard violated: productInCart > 0");
                ProductInCart--;
                break;

            case "clickClearCart":
                ProductInCart = 0;
                break;

            case "addPromoCode":
                IsPromoCodeActivated = true;
                break;

            case "removePromoCode":
                IsPromoCodeActivated = false;
                break;

            case "clickPlaceOrder":
                if (ProductInCart <= 0)
                    throw new InvalidOperationException("EFSM guard violated: productInCart > 0");

                IsPromoCodeActivated = false;
                ProductInCart = 0;
                break;

            default:
                break;
        }
    }
}