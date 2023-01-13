using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBookDB", menuName = "Custom Scriptable Object/CreateNewBookDB")]
public class BookDB : ScriptableObject
{
    [SerializeField]
    public List<BookItem> books;

    public Dictionary<string, BookItem> booksDict;

    private void OnEnable() {
        booksDict = new Dictionary<string, BookItem>();
        foreach (BookItem book in books) {
            booksDict.Add(book.bookID, book);
        }
    }
}
