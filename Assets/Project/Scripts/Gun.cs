//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class Gun : MonoBehaviour {

////	#region Delegate
////	public delegate void OnShoot(int currentAmmo, int ammoAmount);
////	public OnShoot onShootCallback;
////	#endregion

//	public int damage = 10;
//	public float range = 100f;
//	public GameObject fpsCamera;
//	public ParticleSystem muzzleFlash;
//	public GameObject impactEffect;
//	public float force = 40f;

//	private float fireRate = 10f;//выстрелов в секунду
//	private float nextTimeToFire = 0f;
//	private AudioSource audioSource;
//	public AudioClip shotSound;
//	public AudioClip noammoSound;
//	public AudioClip reloadSound;

//	private int maxAmmo = 10;
//	private int currentAmmo;
//	private float reloadtime = 3f;
//	private bool isReloading = false;
//	public Animator weaponAnimator;

//	private bool isAiming = false;
//	public Camera mainCamera;
//	private float scopedFOV = 15f;
//	private float normalFOV = 35f;
//	public GameObject weaponHolder;//для отключения скрипта смены оружия при перезарядке и прицеливании
//	private int ammoAmount = 30;

//	public Text currentAmmoText;
//	public Text ammoAmountText;

//	public GameObject bulletHoleDecal;

//	void Start()
//	{
//		audioSource = GetComponent<AudioSource> ();
//		currentAmmo = maxAmmo;
//		UpdateAmmoUI ();
//	}

//	void OnEnable() //для обновления скрипта при переключении оружия
//	{
//		isReloading = false;
//		isAiming = false;
//		weaponAnimator.SetBool ("isReloading", false);
//		weaponAnimator.SetBool ("isAiming", false);
//		mainCamera.fieldOfView = normalFOV;
//	}

//	void Update () {

//		if (Input.GetButtonDown ("Fire2") && !isReloading) {
//			Aim ();
//		}

//		if (isReloading)
//			return;

//		if (Input.GetKeyDown(KeyCode.R) && !isAiming && currentAmmo < maxAmmo) {
//			StartCoroutine (Reload ());
//			return;
//		}
			
//		if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !isReloading && ammoAmount > 0)
//			{
//				nextTimeToFire = Time.time + 1f / fireRate;
//				Shoot();
//			}
//	}

//	IEnumerator Reload()
//	{
//		weaponHolder.GetComponent<WeaponSwitching> ().enabled = false;
//		isReloading = true;
//		audioSource.PlayOneShot (reloadSound);
//		weaponAnimator.SetBool ("isReloading", true);

//		yield return new WaitForSeconds (reloadtime - .25f);

//		weaponAnimator.SetBool ("isReloading", false);

//		yield return new WaitForSeconds (.25f);

//		if (ammoAmount != 0) {
//			if (ammoAmount >= maxAmmo) {
//				currentAmmo = maxAmmo;
//			} else
//				currentAmmo = ammoAmount;
//		}

//		isReloading = false;
//		weaponHolder.GetComponent<WeaponSwitching> ().enabled = true;
//		UpdateAmmoUI ();
////		if (onShootCallback != null) {
////			onShootCallback.Invoke (currentAmmo, ammoAmount);
////		}
//	}

//	void Shoot()
//	{
//		if (currentAmmo <= 0) 
//		{
//			nextTimeToFire = Time.time + 1f; //чтобы звук клика был не раньше, чем через 1 сек.
//			audioSource.PlayOneShot (noammoSound);
//		} 

//		else 
//		{
//			currentAmmo--;
//			ammoAmount--;
//			muzzleFlash.Play ();
//			audioSource.PlayOneShot (shotSound);
//			RaycastHit hit;
//			if (Physics.Raycast (fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range)) {
//				EnemyStats enemyTarget = hit.transform.GetComponent<EnemyStats> ();
//				if (enemyTarget != null) {
//					enemyTarget.TakeDamage (damage);
////					Debug.Log (enemyTarget.currentHealth);
//				}

//				if (hit.rigidbody != null) {
//					hit.rigidbody.AddForce (-hit.normal * force);//TODO
////					hit.rigidbody.AddForceAtPosition (-hit.normal * force, hit.transform.InverseTransformPoint(hit.point), ForceMode.Impulse);
//				}

//				GameObject g = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
//				GameObject g2 = Instantiate (bulletHoleDecal, hit.point + hit.normal * 0.01f, Quaternion.LookRotation (-hit.normal));
//				g2.transform.SetParent (hit.transform);

//				Destroy (g, 2f);
//				Destroy (g2, 15f);
//			}
//			UpdateAmmoUI ();

////			if (onShootCallback != null) {
////				onShootCallback.Invoke (currentAmmo, ammoAmount);
////			}

//		}
//	}

//	void Aim()
//	{
//		isAiming = !isAiming;
//		weaponAnimator.SetBool ("isAiming", isAiming);
//		weaponHolder.GetComponent<WeaponSwitching> ().enabled = !isAiming;
//		if (isAiming)
//			mainCamera.fieldOfView = scopedFOV;
//		else
//			mainCamera.fieldOfView = normalFOV;
//	}

//	void UpdateAmmoUI()
//	{
//		currentAmmoText.text = currentAmmo.ToString ();
//		ammoAmountText.text = ammoAmount.ToString ();
//	}
//}
