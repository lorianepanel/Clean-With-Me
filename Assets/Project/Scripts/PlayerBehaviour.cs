using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI batteryText; // TextMeshPro pour afficher le niveau de la batterie
    [SerializeField] private float batteryLife = 60f; // Durée de la batterie en secondes
    [SerializeField] private float lowBatteryThreshold = 20f; // Seuil de batterie faible en pourcentage
    [SerializeField] private AudioSource lowBatterySound; // Son à jouer lorsque la batterie est faible
    [SerializeField] private AudioSource succionSound; 
    [SerializeField] private PlayerMovement playerMovement; // Référence au script de mouvement du joueur
    [SerializeField] private TextMeshProUGUI gameOverText; // TextMeshPro pour afficher "Game Over"

    private float currentBatteryLife; // Durée de vie actuelle de la batterie
    private bool isLowBatterySoundPlayed = false; // Indique si le son de batterie faible a déjà été joué
    private bool isGameOver = false; // Indique si le jeu est terminé

    private void Start()
    {
        currentBatteryLife = batteryLife;

        // Affiche la durée de vie initiale de la batterie dans le TextMeshPro
        UpdateBatteryText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            PlaySuccionSound();
            CollectItem(other.gameObject);
        }
    }

    private void Update()
    {
        if (!isGameOver)
        {
            // Décrémente le temps de vie de la batterie
            currentBatteryLife -= Time.deltaTime;

            // Vérifie si la batterie est à zéro
            if (currentBatteryLife <= 0)
            {
                isGameOver = true;
                StartCoroutine(GameOverRoutine());
            }
            else
            {
                // Vérifie si la batterie est faible et déclenche le son de batterie faible
                if (currentBatteryLife <= lowBatteryThreshold && !isLowBatterySoundPlayed)
                {
                    isLowBatterySoundPlayed = true;
                    PlayLowBatterySound();
                }
            }

            // Met à jour le texte de la batterie
            UpdateBatteryText();
        }
    }

    private void CollectItem(GameObject collectible)
    {
        string itemName = collectible.name;
        Debug.Log("Collected : " + itemName);

        // Affiche le nom de l'objet collecté en utilisant TextMeshPro
        if (itemDescription != null)
        {
            itemDescription.text = "Collected : " + itemName;
        }

        collectible.SetActive(false);
    }

    private void PlayLowBatterySound()
    {
        if (lowBatterySound != null)
        {
            lowBatterySound.Play();
        }
    }

    private void PlaySuccionSound()
    {
        if (succionSound != null)
        {
            succionSound.Play();
        }
    }

    private void UpdateBatteryText()
    {
        if (batteryText != null)
        {
            float batteryPercentage = (currentBatteryLife / batteryLife) * 100f; // Calcul du pourcentage restant de la batterie
            batteryText.text = "Battery: " + Mathf.RoundToInt(batteryPercentage).ToString() + "%"; // Met à jour le texte avec le pourcentage restant de la batterie
        }
    }

    private IEnumerator GameOverRoutine()
    {
        // Affiche "Game Over" dans le TextMeshPro indépendant
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }


        // Désactive le script de mouvement du joueur
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Attend quelques secondes avant de charger la scène de crédits
        yield return new WaitForSeconds(10f);

        // Charge la scène de crédits
        SceneManager.LoadScene("CreditsScene");
    }
}
