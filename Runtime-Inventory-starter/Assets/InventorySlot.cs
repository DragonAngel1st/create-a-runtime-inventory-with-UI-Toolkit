using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public Image Icon;
    public string ItemGuid = "";

    public InventorySlot()
    {
        //Create a new Image elmeent and add it to the root
        Icon = new Image();
        Add(Icon);

        //Add USS Style properties to the elements
        Icon.AddToClassList("slotIcon");
        AddToClassList("slot_container");

        RegisterCallback<PointerDownEvent>(OnPointerDown);

    }

    public void HoldItem(ItemDetails item)
    {
        Icon.image = item.Icon.texture;
        ItemGuid = item.GUID;
    }
    public void DropItem()
    {
        ItemGuid = "";
        Icon.image = null;
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button
        if (evt.button != 0 || ItemGuid.Equals(""))
        {
            return;
        }

        //Clear the image
        Icon.image = null;

        Debug.Log("Picked-up slot.ItemGuid: " + ItemGuid);

        //Start the drag
        InventoryUIController.StartDrag(evt.position, this);
    }


    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion

}

