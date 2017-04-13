using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class SlotControl : MonoBehaviour, IDropHandler {

    private InventoryChanger inventoryChanger;

    void Start()
    {
        inventoryChanger = FindObjectOfType<InventoryChanger>();
    }

    public GameObject item
    {
        get
        {
            if(transform.childCount > 0)
            {
                print("Returning "+transform.GetChild(0).gameObject+" from SlotControl");
                return transform.GetChild(0).gameObject;
            }
            print("Returning null from SlotControl");
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if(!item)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            //ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
            inventoryChanger.HasChanged();
        }
    }
    #endregion
}
