// başlangıçta arac sayısı kadar arac Instantiate ediyor - araç sayısı ınpector den elle girilecek
// Instantiate edilecek aracları da Araçlar Listesinden rastgel çekiyor. - araç listesi ınspectorden elle doldurulacak. yeni araclar eklemesi böylece çok kolay.



using System.Collections.Generic;
using UnityEngine;

public class AracBaslangic : MonoBehaviour
{
    [SerializeField] int AracSayisi;
    [SerializeField] GameObject[] Araclar;
    

    private void Awake()
    {
        for (int i=0; i < AracSayisi; i++)
        {
            int a = Random.Range(0, Araclar.Length);
            Instantiate(Araclar[a], transform.position, transform.rotation, transform);
            

        }
    }
}
