using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    #region Variables & References
    public bool isInBuildMode = false;
    public Buildable testBlock;
    public Buildable previewBlock;
    public Material previewMaterialGood;
    public Material previewMaterialBad;
    public LayerMask buildSurfaceMask;
    Renderer previewRenderer;
    #endregion

    #region startup methods
    private void OnEnable()
    {
        PathManager.onNewPathCalculated += SetPreviewMaterial;
    }
    private void OnDisable()
    {
        PathManager.onNewPathCalculated -= SetPreviewMaterial;
    }
    #endregion

    #region Preview Methods
    void InstantiatePreviewBuildable()
    {
        previewBlock = Instantiate(testBlock);
        previewRenderer = previewBlock.GetComponent<Renderer>();
        previewBlock.GetComponent<Renderer>().material = previewMaterialGood;
    }
    void SetPreviewMaterial(bool isBlockViable)
    {
        if (isBlockViable)
        {
            previewRenderer.material = previewMaterialGood;
        }
        else
        {
            previewRenderer.material = previewMaterialBad;
        }
    }

    void MovePreviewBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, buildSurfaceMask);
        if (hit.collider)
        {
            if (hit.collider.tag.Equals("Surface"))
            {
                Vector3 gridPosition = BuildManager.GetGridPoint(hit.point);

                if (previewBlock.transform.position != gridPosition)
                {
                    previewBlock.transform.position = gridPosition;

                    Debug.Log("Calculating");
                    PathManager.instance.CalculateNewPaths();
                }
            }


        }
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInBuildMode = true;
            InstantiatePreviewBuildable();
        }
        if (Input.GetMouseButtonDown(1))
        {
            isInBuildMode = false;
            //Destroy(previewBlock);
        }

        if (!isInBuildMode)
        {
            return;
        }

        MovePreviewBlock();
    }
}
