using UnityEngine;
using Zenject;

public class MetaDebugTools : MonoBehaviour
{
    private MetaProgressionService _metaService;

    [Inject]
    public void Construct(MetaProgressionService metaService)
    {
        _metaService = metaService;
    }

    [ContextMenu("Reset Meta Progression")]
    public void ResetMeta()
    {
        _metaService.ResetProgression();
        Debug.Log("🔥 Meta progression reset!");
    }
}