
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty
    {
        VeryEasy,Easy,Normal,Hard,VeryHard
    }

    [System.Serializable]
    public class DifficultySettings
    {
        [Header("Player")]
        public float forwardSpeed = 5f;
        public int initialSticks = 5;

        [Header("Path")]
        public int sticksPerChunk = 15;

        [Range(0f, 1f)]
        public float gapChance = 0.3f;

        public int minRequiredSticks = 5;
        public int maxRequiredSticks = 10;
    }

    [Header("Starting Difficulty")]
    [SerializeField] private Difficulty selectedDifficulty =
        Difficulty.Normal;

    [Header("Progressive Difficulty")]
    [SerializeField] private bool progressiveDifficulty = true;
    [SerializeField] private float distanceToMaxDifficulty = 500f;

    [Header("Very Easy")]
    [SerializeField] private DifficultySettings veryEasy =
        new DifficultySettings
        {
            forwardSpeed = 3f,
            initialSticks = 10,
            sticksPerChunk = 20,
            gapChance = 0.1f,
            minRequiredSticks = 2,
            maxRequiredSticks = 4
        };

    [Header("Easy")]
    [SerializeField] private DifficultySettings easy =
        new DifficultySettings
        {
            forwardSpeed = 4f,
            initialSticks = 8,
            sticksPerChunk = 18,
            gapChance = 0.15f,
            minRequiredSticks = 3,
            maxRequiredSticks = 6
        };

    [Header("Normal")]
    [SerializeField] private DifficultySettings normal =
        new DifficultySettings
        {
            forwardSpeed = 5f,
            initialSticks = 5,
            sticksPerChunk = 15,
            gapChance = 0.3f,
            minRequiredSticks = 5,
            maxRequiredSticks = 10
        };

    [Header("Hard")]
    [SerializeField] private DifficultySettings hard =
        new DifficultySettings
        {
            forwardSpeed = 6f,
            initialSticks = 3,
            sticksPerChunk = 12,
            gapChance = 0.45f,
            minRequiredSticks = 6,
            maxRequiredSticks = 12
        };

    [Header("Very Hard")]
    [SerializeField] private DifficultySettings veryHard =
        new DifficultySettings
        {
            forwardSpeed = 7f,
            initialSticks = 2,
            sticksPerChunk = 10,
            gapChance = 0.6f,
            minRequiredSticks = 8,
            maxRequiredSticks = 15
        };

    [Header("Progressive Limits")]
    [SerializeField] private float maxForwardSpeed = 9f;

    [Range(0f, 1f)]
    [SerializeField] private float maxGapChance = 0.75f;

    [SerializeField] private int maxMinRequiredSticks = 10;
    [SerializeField] private int maxMaxRequiredSticks = 18;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private StickInventory stickInventory;
    [SerializeField] private PathGenerator pathGenerator;

    private DifficultySettings currentSettings;
    private float startZ;
    private float lastProgress = -1f;

    private void Awake()
    {
        currentSettings = GetSelectedSettings();

        if (player != null)
            startZ = player.position.z;

        ApplyInitialDifficulty();
    }

    private DifficultySettings GetSelectedSettings()
    {
        switch (selectedDifficulty)
        {
            case Difficulty.VeryEasy:
                return veryEasy;

            case Difficulty.Easy:
                return easy;

            case Difficulty.Hard:
                return hard;

            case Difficulty.VeryHard:
                return veryHard;

            default:
                return normal;
        }
    }

    private void ApplyInitialDifficulty()
    {
        if (playerMovement != null)
            playerMovement.SetForwardSpeed(
                currentSettings.forwardSpeed);

        if (stickInventory != null)
            stickInventory.SetInitialSticks(
                currentSettings.initialSticks);

        if (pathGenerator != null)
        {
            pathGenerator.SetInitialSettings(
                currentSettings.sticksPerChunk,
                currentSettings.gapChance,
                currentSettings.minRequiredSticks,
                currentSettings.maxRequiredSticks);
        }
    }

    private void Update()
    {
        if (!progressiveDifficulty || player == null)
            return;

        float distance = Mathf.Max(
            0f, player.position.z - startZ);

        float progress = distanceToMaxDifficulty <= 0f
            ? 1f
            : Mathf.Clamp01(distance / distanceToMaxDifficulty);

        if (Mathf.Approximately(progress, lastProgress))
            return;

        lastProgress = progress;

        float speed = Mathf.Lerp(
            currentSettings.forwardSpeed,
            Mathf.Max(currentSettings.forwardSpeed, maxForwardSpeed),
            progress);

        float gapChance = Mathf.Lerp(
            currentSettings.gapChance,
            Mathf.Max(currentSettings.gapChance, maxGapChance),
            progress);

        int minRequired = Mathf.RoundToInt(Mathf.Lerp(
            currentSettings.minRequiredSticks,
            Mathf.Max(currentSettings.minRequiredSticks,
                maxMinRequiredSticks),
            progress));

        int maxRequired = Mathf.RoundToInt(Mathf.Lerp(
            currentSettings.maxRequiredSticks,
            Mathf.Max(currentSettings.maxRequiredSticks,
                maxMaxRequiredSticks),
            progress));

        maxRequired = Mathf.Max(maxRequired, minRequired);

        if (playerMovement != null)
            playerMovement.SetForwardSpeed(speed);

        if (pathGenerator != null)
        {
            pathGenerator.SetGapSettings(
                gapChance, minRequired, maxRequired);
        }
    }
}
