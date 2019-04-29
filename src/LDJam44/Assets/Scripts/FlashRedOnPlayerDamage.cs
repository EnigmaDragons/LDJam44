using UnityEngine;
using UnityEngine.UI;

class FlashRedOnPlayerDamage : VerboseMonoBehaviour
{
    public Health Health;

    private Image image;
    private Color targetColor;
    private Color targetTransparent;

    bool wasFreshlyDamaged;

    void Start()
    {
        image = VerboseGetComponent<Image>();
        targetColor = new Color(image.color.r, image.color.g, image.color.b, 1f);
        targetTransparent = new Color(image.color.r, image.color.g, image.color.b, 0f);
        Health.OnDamage(() => wasFreshlyDamaged = true);
    }

    void Update()
    {
        if (wasFreshlyDamaged)
            image.color = Color.Lerp(image.color, targetColor, 20 * Time.deltaTime);
        else
            image.color = Color.Lerp(image.color, targetTransparent, 20 * Time.deltaTime);

        if (image.color.a >= 0.6)
            wasFreshlyDamaged = false;
    }
}
