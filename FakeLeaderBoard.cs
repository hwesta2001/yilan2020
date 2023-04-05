using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class FakeLeaderBoard : MonoBehaviour
{
    public static FakeLeaderBoard instance;

    //[SerializeField] private TextMeshProUGUI score1, score2, score4, score5;
    //[SerializeField] private TextMeshProUGUI[] nums;
    //[SerializeField] private Image image1, image2, imageYours, image4, image5;
    //[SerializeField] private Sprite[] pictures;
    //[SerializeField] private String[] names, surnames;



    private void Awake()
    {
        instance = this;
    }



    public void FakeBoardYap(int scr)
    {
        
    }







    //void ResimleriDegistir()
    //{
    //    image1.sprite = pictures[Random.Range(1, pictures.Length)];
    //    image2.sprite = pictures[Random.Range(1, pictures.Length)];
    //    imageYours.sprite = pictures[0];
    //    image1.sprite = pictures[Random.Range(1, pictures.Length)];
    //    image4.sprite = pictures[Random.Range(1, pictures.Length)];
    //    image5.sprite = pictures[Random.Range(1, pictures.Length)];

    //}

    //void NumaraDegistir(int score)
    //{
    //    if (score > 15000)
    //    {
    //        int i = Random.Range(1, 10);
    //        nums[0].text = $"{i}";
    //        nums[1].text = $"{i + 1}";
    //        nums[2].text = $"{i + 2}";
    //        nums[3].text = $"{i + 3}";
    //        nums[4].text = $"{i + 4}";
    //    }
    //    else if (score > 8500)
    //    {
    //        int i = Random.Range(10, 150);
    //        nums[0].text = $"{i}";
    //        nums[1].text = $"{i + 1}";
    //        nums[2].text = $"{i + 2}";
    //        nums[3].text = $"{i + 3}";
    //        nums[4].text = $"{i + 4}";
    //    }
    //    else if (score > 3500)
    //    {
    //        int i = Random.Range(100, 1000);
    //        nums[0].text = $"{i}";
    //        nums[1].text = $"{i + 1}";
    //        nums[2].text = $"{i + 2}";
    //        nums[3].text = $"{i + 3}";
    //        nums[4].text = $"{i + 4}";
    //    }
    //    else 
    //    {
    //        int i = Random.Range(360, 2000);
    //        nums[0].text = $"{i}";
    //        nums[1].text = $"{i + 1}";
    //        nums[2].text = $"{i + 2}";
    //        nums[3].text = $"{i + 3}";
    //        nums[4].text = $"{i + 4}";
    //    }
    //}


    //void NameDegistir(int score)
    //{

    //    int a = score + Random.Range(100, 1000);
    //    int aa = score + Random.Range(10, 100);
    //    int b = score + Random.Range(-100, -10);
    //    int bb = score + Random.Range(-1000, -100);

    //   // a = (int)Mathf.Clamp(a, 0, Mathf.Infinity);
    //  //  aa = (int)Mathf.Clamp(aa, 0, Mathf.Infinity);
    //    b = (int)Mathf.Clamp(b, 0, Mathf.Infinity);
    //    bb = (int)Mathf.Clamp(bb, 0, Mathf.Infinity);




    //    score1.text = names[Random.Range(0, names.Length)] + $" " + surnames[Random.Range(0, names.Length)] + $" : {a}";
    //    score2.text = names[Random.Range(0, names.Length)] + $" " + surnames[Random.Range(0, names.Length)] + $" : {aa}";
    //    score4.text = names[Random.Range(0, names.Length)] + $" " + surnames[Random.Range(0, names.Length)] + $" : {b}";
    //    score5.text = names[Random.Range(0, names.Length)] + $" " + surnames[Random.Range(0, names.Length)] + $" : {bb}";
    //}

}
