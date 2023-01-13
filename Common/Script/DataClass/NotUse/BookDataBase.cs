//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "NewBookDataBase", menuName = "Custom Scriptable Object/Create New Book Database")]
//public class BookDataBase : ScriptableObject {
//    public List<BookObject> books;

//    public Dictionary<string, BookObject> booksDict;

//    private void OnEnable() {
//        booksDict = new Dictionary<string, BookObject>();
//        foreach (BookObject book in books) {
//            booksDict.Add(book.bookID, book);
//        }
//    }
//}