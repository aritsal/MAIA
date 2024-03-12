using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Recipe : MonoBehaviour {
    [SerializeField] private List<Ingredient> _ingredients = new List<Ingredient>();
    
    // If there are no inactive ingridients, it's done
    public bool isComplete => this._ingredients.FindIndex(i => !i.isActive) != -1;


    public Item[] GetMissingItems() {
        return this._ingredients.Where((i) => !i.isActive).Select(i => i.item).ToArray();
    }

    private void Start() {
        this._ingredients.ForEach((i) => {
            if (!i.isActive)
                i.gameObject.SetActive(false);
            else {
                i.gameObject.SetActive(true);
            }
        });

        this.GetComponent<Interactable>().onInteractKeyUp += (i) => this.TryAddItem(i.handSlot.currentItem);
    }

    private void TryAddItem(ItemRef itemRef) {
        if (itemRef == null) return;

        Ingredient ingredient = this._ingredients.Find((i) => i.item == itemRef.item);

        if (ingredient == null || ingredient.isActive) return;

        ingredient.isActive = true;
        ingredient.gameObject.SetActive(true);
        GameObject.Destroy(itemRef.gameObject);
    }
}