using UnityEngine;
using UnityEngine.UI;

public class RulesNavigation : MonoBehaviour
{
    public GameObject[] rulePanels; // Tableau contenant tous les panneaux des règles
    private int currentPanelIndex = 0; // Index du panneau actuel
    public GameObject menuPanel;
    public Button nextButton;   // Bouton "Next"
    public Button prevButton;   // Bouton "Previous"

    private void Start()
    {
        // Désactivez tous les panneaux sauf le premier
        foreach (GameObject panel in rulePanels)
        {
            panel.SetActive(false);
        }

        // Activez le premier panneau
        rulePanels[currentPanelIndex].SetActive(true);

        // Ajouter des listeners aux boutons
        nextButton.onClick.AddListener(ShowNextPanel);
        prevButton.onClick.AddListener(ShowPreviousPanel);

        // Désactiver le bouton "Previous" sur le premier panneau
        prevButton.interactable = false;
        nextButton.onClick.AddListener(BackToMenu);
    }

    private void ShowNextPanel()
    {
        // Désactivez le panneau actuel
        rulePanels[currentPanelIndex].SetActive(false);

        // Incrémentez l'index pour aller au suivant
        currentPanelIndex++;

        // Si l'on est au dernier panneau, on désactive le bouton "Next"
        if (currentPanelIndex >= rulePanels.Length - 1)
        {
            nextButton.interactable = false;
        }

        // Activez le panneau suivant
        rulePanels[currentPanelIndex].SetActive(true);

        // Activez le bouton "Previous" si ce n'est pas le premier panneau
        prevButton.interactable = true;
    }

    private void ShowPreviousPanel()
    {
        // Désactivez le panneau actuel
        rulePanels[currentPanelIndex].SetActive(false);

        // Décrémentez l'index pour revenir au panneau précédent
        currentPanelIndex--;

        // Si l'on est au premier panneau, on désactive le bouton "Previous"
        if (currentPanelIndex <= 0)
        {
            prevButton.interactable = false;
        }

        // Activez le panneau précédent
        rulePanels[currentPanelIndex].SetActive(true);

        // Activez le bouton "Next" si ce n'est pas le dernier panneau
        nextButton.interactable = true;
    }
    
    private void BackToMenu()
    {
        // Cachez tous les panneaux de règles
        foreach (GameObject panel in rulePanels)
        {
            panel.SetActive(false);
        }

        // Affichez le menu principal
        menuPanel.SetActive(true);
    }
}
