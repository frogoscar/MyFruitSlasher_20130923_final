using UnityEngine;
using System.Collections;

public class ShowHand : MonoBehaviour {
	
	/* Interact with Intel Perceptual SDK */
	private Texture2D handImage;
	private byte[] labelmap;
	public  PXCMGesture.GeoNode[][] handData;
	private PXCUPipeline pp = null;
	private PXCMGesture.GeoNode[][] mouse_nodes;
	
	/*
	* When Thumb Finger and Index Finger does a convergence (distance == 0), we consider it as a mouse down, 
	* get the first position at that moment (the middle in mittened hand), 
	* When they are apart, we consider it as a mouse down, get the second position at that moment 
	* (the middle point of the thumb finger and index finger)
	*/
	public static bool isMouseDown;
	public static Vector2 pMiddleThumbIndex;
	public static Vector2 pFingerIndex;
	public static Vector2 pFingerThumb;
	public static Vector2 pHandMiddle;
	
	public Options options;
	
	void Start() {
		pp=new PXCUPipeline();
		
		int[] size=new int[2]{320,240};
		if (pp.Init(PXCUPipeline.Mode.GESTURE)) {
	        pp.QueryLabelMapSize(size);
	        print("LabelMap: width=" + size[0] + ", height=" + size[1]);
			labelmap=new byte[size[0]*size[1]];
		} else {
			options.SetMessage("Failed to detect the Creative* camera. Please plugin the camera and click restart.");
		}
		handImage=new Texture2D(size[0],size[1],TextureFormat.ARGB32,false);
		ZeroImage(handImage);
		handData=new PXCMGesture.GeoNode[2][];
		handData[0]=new PXCMGesture.GeoNode[6];
		handData[1]=new PXCMGesture.GeoNode[6];
		
		mouse_nodes = new PXCMGesture.GeoNode[1][]{ new PXCMGesture.GeoNode[12]};
	}
	
	void OnDisable() {
		if (pp==null) return;
		pp.Dispose();
		pp=null;
    }
	
	void OnGUI () {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), handImage, ScaleMode.StretchToFill);
    }
	
	void Update () {
		if (pp!=null) {
			if (pp.AcquireFrame(false)) {
				print(pp.IsDisconnected()?"Camera unplugged? Please replugin the camera to the same USB port. Thanks.":null);
				
				int[] labels=new int[3]{0,256,256};
				pp.QueryLabelMap(labelmap,labels);
			    Color32[] pixels=handImage.GetPixels32(0);
				for (int y=0, yy1=0, yy2=(handImage.height-1)*handImage.width;y<handImage.height;y++,yy1+=handImage.width,yy2-=handImage.width) {
					for (int x=0;x<handImage.width;x++) {
						int pixel=labelmap[yy1+x];
						pixels[yy2+(handImage.width-1-x)]=new Color32(0,0,0,(byte)((pixel==labels[1] || pixel==labels[2])?160:0));
					}
				}
		        handImage.SetPixels32 (pixels, 0);
				handImage.Apply();
				
				pp.QueryGeoNode(PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_SECONDARY,handData[0]);
				pp.QueryGeoNode(PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY,handData[1]);
				
				pp.QueryGeoNode(PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, mouse_nodes[0]);
				pFingerIndex = GetGeonodeCoordinate(mouse_nodes, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, PXCMGesture.GeoNode.Label.LABEL_FINGER_INDEX);
				pFingerThumb = GetGeonodeCoordinate(mouse_nodes, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, PXCMGesture.GeoNode.Label.LABEL_FINGER_THUMB);
				pHandMiddle = GetGeonodeCoordinate(mouse_nodes, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, PXCMGesture.GeoNode.Label.LABEL_HAND_MIDDLE);
				
				pMiddleThumbIndex = new Vector2((pFingerThumb.x + pFingerIndex.x) / 2, (pFingerThumb.y + pFingerIndex.y) / 2);
				isMouseDown = (DistanceBetweenPoints(pFingerThumb, pFingerIndex) == 0) ? true : false;
				
				Vector3 worldPositionThumb = Camera.main.ScreenToWorldPoint(new Vector3(pFingerThumb.x, pFingerThumb.y, 1));
				Vector3 worldPositionIndex = Camera.main.ScreenToWorldPoint(new Vector3(pFingerIndex.x, pFingerIndex.y, 1));
				Vector3 worldPositionHandMiddle = Camera.main.ScreenToWorldPoint(new Vector3(pHandMiddle.x, pHandMiddle.y, 1));
				
//				options.SetMessage("ImagePosition of Thumb Finger : (" + pFingerThumb.x + ", " + pFingerThumb.y + ")\n\n" +
//                    "ImagePosition of Index Finger : (" + pFingerIndex.x + ", " + pFingerIndex.y + ")\n\n" +
//					"ImagePosition of middle in mittened hand : (" + pHandMiddle.x + ", " + pHandMiddle.y + ")\n\n" +
//                    "Distance between Thumb Finger and Index Finger : " + DistanceBetweenPoints(pFingerThumb, pFingerIndex) + "\n\n" +
//					"WorldPosition of Thumb Finger : (" + worldPositionThumb.x + ", " + worldPositionThumb.y + ")\n\n" +
//					"WorldPosition of Index Finger : (" + worldPositionIndex.x + ", " + worldPositionIndex.y + ")\n\n" +
//					"WorldPosition of middle in mittened hand : (" + worldPositionHandMiddle.x + ", " + worldPositionHandMiddle.y);
				
				pp.ReleaseFrame();
			}
		}
	}
	
	private void ZeroImage(Texture2D image) {
		Color32[] pixels=image.GetPixels32(0);
		for (int x=0;x<image.width*image.height;x++) pixels[x]=new Color32(255,255,255,128);
	    image.SetPixels32(pixels, 0);
		image.Apply();
	}
	
	public Vector3 MapCoordinates(PXCMPoint3DF32 pos1) {
		Vector3 pos2=Camera.main.ViewportToWorldPoint(new Vector3((float)(handImage.width-1-pos1.x)/handImage.width,(float)(handImage.height-1-pos1.y)/handImage.height,0));
		pos2.z=0;
		return pos2;
	}
	
	private float DistanceBetweenPoints(Vector2 point1, Vector2 point2) {
		return Mathf.Sqrt(Mathf.Pow(point2.x - point1.x, 2.0f) + Mathf.Pow(point2.y - point1.y, 2.0f));
	}
	
	private bool IsPointNull(Vector2 point) {
		if (point.x == 0 && point.y == 0) {
			return true;
		}
		else {
			return false;
		}
	}
	
	private Vector2 GetGeonodeCoordinate(PXCMGesture.GeoNode[][] nodes, PXCMGesture.GeoNode.Label handLabel, PXCMGesture.GeoNode.Label nodeLabel) {
		Vector2 returnPoint = new Vector2(0, 0);
		for (int i = 0; i < 12; i++) {
			if (nodes[0][i].body == (handLabel | nodeLabel)) {
				returnPoint.x = (int)nodes[0][i].positionImage.x;
				returnPoint.y = (int)nodes[0][i].positionImage.y;
			}
		}
		return returnPoint;
	}
}
