using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

public class HKAlignerTool : HKTool
{
    public override string toolName => "Object Aligner";
    public override string iconName => "Aligner_Icon";

    Transform[] selectionTransforms = new Transform[0];

    public override void Enter()
    {
        Selection.selectionChanged += SelectionChange;
        SelectionChange();
    }

    public override void Exit()
    {
        Selection.selectionChanged -= SelectionChange;
    }

    void SelectionChange()
    {
        selectionTransforms = Selection.transforms;
        Draw();
    }

    void Draw()
    {
        page.Clear();
        DrawScrollView();

        if (selectionTransforms.Length == 0) return;
        DrawButton(page, AlignToSurface, "Align");


        void DrawScrollView()
        {
            Label selectedObjectLabel = new Label($"({selectionTransforms.Length}) Selected Objects");
            selectedObjectLabel.style.paddingLeft = 10;
            selectedObjectLabel.style.paddingBottom = 5;
            page.Add(selectedObjectLabel);

            ScrollView selectedObjectsPreviewScroller = new ScrollView(ScrollViewMode.VerticalAndHorizontal);
            selectedObjectsPreviewScroller.style.minHeight = 0f;

            selectedObjectsPreviewScroller.style.backgroundColor = new Color(0.19f, 0.19f, 0.19f);
            selectedObjectsPreviewScroller.style.paddingLeft = 10;
            page.Add(selectedObjectsPreviewScroller);

            foreach (Transform t in selectionTransforms)
            {
                Label selectionInfoLabel = new Label(t.name);

                selectedObjectsPreviewScroller.Add(selectionInfoLabel);
            }
        }
    }

    void AlignToSurface()
    {
        Undo.RecordObjects(selectionTransforms, "Align Objects With HK Aligner Tool");

        foreach (Transform t in selectionTransforms)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in selectionTransforms)
        {
            if (Physics.Raycast(t.position + Vector3.up * 0.01f, Vector3.down, out RaycastHit hit, 100f))
            {
                t.position = hit.point;
                t.up = hit.normal;
            }
        }

        foreach (Transform t in selectionTransforms)
        {
            t.gameObject.SetActive(true);
        }
    }
}