using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ImageSizeHandler : MonoBehaviour
{
    [SerializeField] private bool middle = false;
    [SerializeField] private RectTransform.Edge stuckEdge = RectTransform.Edge.Left;
    [SerializeField] private Image image = null;
    private RectTransform imageRect = null;
    private RectTransform parent = null;

    private void Awake()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
            if(image == null)
            {
                return;
            }
        }
        imageRect = image.GetComponent<RectTransform>();
        parent = this.transform.parent.gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
            if (image == null)
            {
                return;
            }
        }

        if (imageRect == null)
        {
            imageRect = image.GetComponent<RectTransform>();
        }
        if (parent == null)
        {
            parent = this.transform.parent.gameObject.GetComponent<RectTransform>();
        }
        bool isTall = image.sprite.rect.height > image.sprite.rect.width ? true : false;
        Vector2 parentAspect = CalculateAspect(parent.rect);
        Vector2 imageAspect = CalculateAspect(image.sprite.rect);

        PlaceAnchors(stuckEdge, middle);

        if (middle)
        {
            double pX = Math.Round(parentAspect.x, 2);
            double iX = Math.Round(imageAspect.x, 2);
            double pY = Math.Round(parentAspect.y, 2);
            double iY = Math.Round(imageAspect.y, 2);
            if (pX == iX && pY == iY)
            {
                imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parent.rect.width);
                imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parent.rect.height);
                imageRect.anchoredPosition = new Vector2(0, 0);
            }
            else if (pX > iX && pY <= iY)
            {
                imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parent.rect.width * imageAspect.x);
                imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parent.rect.height);
                imageRect.anchoredPosition = new Vector2(0, 0);
            }
            else if (pX <= iX && pY > iY)
            {
                imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parent.rect.width);
                imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parent.rect.height * imageAspect.x);
                imageRect.anchoredPosition = new Vector2(0, 0);
            }
        }
        else
        {
            bool wasTooBig = SetSize(imageAspect, isTall);
            PositionImage(stuckEdge, isTall, imageAspect, wasTooBig);
        }
    }

    private void PositionImage(RectTransform.Edge stuckEdge, bool isTall, Vector2 aspect, bool wasTooBig)
    {
        switch (stuckEdge)
        {
            case RectTransform.Edge.Left:
                if (isTall && !wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(parent.rect.height / 2 * aspect.y, 0);
                }
                else if(!isTall && !wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(parent.rect.width / 2, 0);
                }
                else if(!isTall && wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(imageRect.rect.width / 2, 0);//Updated
                }
                else
                {
                    imageRect.anchoredPosition = new Vector2(parent.rect.width / 2, 0);
                }
                break;
            case RectTransform.Edge.Right:
                if (isTall && !wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(-parent.rect.height / 2 * aspect.y, 0);
                }
                else if (!isTall && !wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(-parent.rect.width / 2, 0);
                }
                else if (!isTall && wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(-imageRect.rect.width / 2, 0); //Updated
                }
                else
                {
                    imageRect.anchoredPosition = new Vector2(parent.rect.width / 2, 0);
                }
                break;
            case RectTransform.Edge.Top:
                if (isTall && !wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(0, -parent.rect.height / 2);
                }
                else if (!isTall && !wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(0, -imageRect.rect.height/ 2);
                }
                else if (!isTall && wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(0, -parent.rect.height / 2);
                }
                else
                {
                    imageRect.anchoredPosition = new Vector2(0, -parent.rect.width / 2 * aspect.x);
                }
                break;
            case RectTransform.Edge.Bottom:
                if (isTall && !wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(0, parent.rect.height / 2);
                }
                else if (!isTall && !wasTooBig)
                {
                    throw new NotImplementedException("This is not done");
                }
                else if (!isTall && wasTooBig)
                {
                    imageRect.anchoredPosition = new Vector2(0, parent.rect.height / 2);
                }
                else
                {
                    imageRect.anchoredPosition = new Vector2(0, parent.rect.width / 2 * aspect.x);
                }
                break;
        }
    }

    private bool SetSize(Vector2 aspect, bool isTall)
    {
        float horizontal;
        float vertical;
        bool wasTooBig = false;
        if (isTall)
        {
            if (parent.rect.height * aspect.y > parent.rect.width)
            {
                horizontal = parent.rect.width;
                vertical = parent.rect.height;
                wasTooBig = true;
            }
            else
            {
                horizontal = parent.rect.height * aspect.y;
                vertical = parent.rect.height;
            }
        }
        else
        {
            // Added check because a 512x512 image in a too small square would be too big (Check booster buying image)
            if(parent.rect.height * aspect.y <= parent.rect.width)
            {
                horizontal = parent.rect.height * aspect.y;
                vertical = parent.rect.height;
                wasTooBig = true;
            }
            else
            {
                horizontal = parent.rect.width;
                vertical = parent.rect.width * aspect.x;
            }
        }
        imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontal );
        imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,vertical); 
        return wasTooBig;
    }

    private void PlaceAnchors(RectTransform.Edge stuckEdge, bool isMiddle)
    {
        if (isMiddle)
        {
            imageRect.anchorMin = new Vector2(0,0);
            imageRect.anchorMax = new Vector2(1, 1);
        }
        else
        {
            Vector2[] combinations = new Vector2[8] {   new Vector2(0, 0), new Vector2(0, 1),
                                                    new Vector2(1, 0), new Vector2(1, 1),
                                                    new Vector2(0, 1), new Vector2(1, 1),
                                                    new Vector2(0, 0), new Vector2(1, 0) };
            imageRect.anchorMin = combinations[(int)stuckEdge * 2];
            imageRect.anchorMax = combinations[(int)stuckEdge * 2 + 1];
        }
    }

    private Vector2 CalculateAspect(Rect rect)
    {
        return new Vector2(rect.height / rect.width, rect.width / rect.height);
    }
}
