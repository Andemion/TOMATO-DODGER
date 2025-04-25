using UnityEngine;
using UnityEngine.UI;

public class RulesNavigation : MonoBehaviour
{
    public GameObject[] rulePanels; // Tableau contenant tous les panneaux des règles
    private int currentPanelIndex = 0; // Index du panneau actuel
    public GameObject menuPanel;
    public Button nextButton;   // Bouton "Next"
    public Button prevButton;   // Bouton "Previous"

    public void OpenRulePanel()
    {
        currentPanelIndex = 0;
        // Désactivez tous les panneaux sauf le premier
        foreach (GameObject panel in rulePanels)
        {
            panel.SetActive(false);
        }

        // Activez le premier panneau et les boutons
        rulePanels[currentPanelIndex].SetActive(true);
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);

        // Ajouter des listeners aux boutons
        nextButton.onClick.AddListener(ShowNextPanel);
        prevButton.onClick.AddListener(ShowPreviousPanel);
    }

    private void ShowNextPanel()
    {
        // Si l'on est à la dernière page, retour au menu
        if (currentPanelIndex == rulePanels.Length - 1)
        {
            BackToMenu();  // Appelle la méthode pour revenir au menu
            return;
        }

        // Désactivez le panneau actuel
        rulePanels[currentPanelIndex].SetActive(false);

        // Incrémentez l'index pour aller au suivant
        currentPanelIndex++;

        // Activez le panneau suivant
        rulePanels[currentPanelIndex].SetActive(true);
    }

    private void ShowPreviousPanel()
    {
        // Si l'on est sur la première page, retour au menu
        if (currentPanelIndex == 0)
        {
            BackToMenu();  // Appelle la méthode pour revenir au menu
            return;
        }

        // Désactivez le panneau actuel
        rulePanels[currentPanelIndex].SetActive(false);

        // Décrémentez l'index pour revenir au panneau précédent
        currentPanelIndex--;

        // Activez le panneau précédent
        rulePanels[currentPanelIndex].SetActive(true);
    }

    private void BackToMenu()
    {
        // Cachez tous les panneaux de règles
        foreach (GameObject panel in rulePanels)
        {
            panel.SetActive(false);
        }

        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        currentPanelIndex = 0;
        // Affichez le menu principal
        menuPanel.SetActive(true);
    }

}
