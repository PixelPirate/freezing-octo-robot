using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GalleryController : MonoBehaviour
{
	bool loadGuard = true;
	bool imageGuard = true;

	IEnumerator Start()
	{
		while (loadGuard) {
			yield return null;
		}

		var images = Resources.LoadAll<TextAsset>(path: "Images");

		while (imageGuard) {
			yield return null;
		}

		foreach (var image in images) {
			var texture = new Texture2D(width: 0, height: 0, format: TextureFormat.ARGB32, mipmap: false);
			texture.LoadImage(data: image.bytes);
			texture.Compress(highQuality: true);
			texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
			var sprite = Sprite.Create(texture: texture, 
			                           rect: new Rect(left: 0f, top: 0f, width: texture.width, height: texture.height), 
			                           pivot: new Vector2(0.5f, 0.5f));
			var imageObject = new GameObject(name: image.name, components: typeof(Image));
			var imageRenderer = imageObject.GetComponent<Image>();
			imageRenderer.sprite = sprite;
			imageObject.transform.parent = transform;
			imageObject.transform.position = Vector3.zero;
			Resources.UnloadAsset(image);
		}

		Resources.UnloadUnusedAssets();
	}

	public void Load()
	{
		loadGuard = false;
	}

	public void Image()
	{
		imageGuard = false;
	}
}
