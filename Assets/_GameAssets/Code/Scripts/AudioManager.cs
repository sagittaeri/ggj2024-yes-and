using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicList;
    public AudioClip[] sfxList;

    public AudioSource musicSource1;
    public AudioSource musicSource2;

    public AudioSource[] sfxSources;

    public Queue<AudioSource> sfxQueue = new Queue<AudioSource>();
    

    private Dictionary<string, AudioClip> musicDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private bool isMusicOne;
    private bool isDestroying;
    private Tween tween;

    static public AudioManager instance;

    void Awake()
    {
        if (instance != null)
        {
            isDestroying = true;
            Destroy(gameObject);
            return;         
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (AudioClip c in musicList)
            musicDict[c.name] = c;
        foreach (AudioClip c in sfxList)
            sfxDict[c.name] = c;
        foreach (AudioSource s in sfxSources)
            sfxQueue.Enqueue(s);
    }

    void Start()
    {
        if (isDestroying)
            return;
        PlaySFX("Intro");
        PlayMusic("Tune 1", delay: 1.8f);
    }

    public void PlayMusic(string name, float crossfadeDuration = 2f, float volume = 1f, float delay = 0f)
    {
        if (!musicDict.ContainsKey(name))
            return;
        AudioClip clip = musicDict[name];

        AudioSource target = isMusicOne ? musicSource2 : musicSource1;
        AudioSource current = null;
        if (isMusicOne && musicSource1.isPlaying)
            current = musicSource1;
        else if (!isMusicOne && musicSource2.isPlaying)
            current = musicSource2;
    
        target.DOKill();
        if (current != null)
            current.DOKill();
        tween?.Kill();

        isMusicOne = !isMusicOne;
        if (current == null)
        {
            tween = DOVirtual.DelayedCall(delay, ()=>
            {
                target.clip = clip;
                target.Play();
            });
        }
        else
        {
            tween = DOVirtual.DelayedCall(delay, ()=>
            {
                current.DOFade(0f, crossfadeDuration).OnComplete(()=>
                {
                    current.Stop();
                    current.clip = null;
                });
                target.clip = clip;
                target.Play();
                target.volume = 0f;
                target.DOFade(volume, crossfadeDuration).OnComplete(()=>
                {
                });
            });
        }
    }

    public void PlaySFX(string name, float volume = 1f, float delay = 0f)
    {
        if (!sfxDict.ContainsKey(name))
            return;
        AudioClip clip = sfxDict[name];

        AudioSource target = sfxQueue.Dequeue();
        sfxQueue.Enqueue(target);

        DOVirtual.DelayedCall(delay, ()=>
        {
            target.clip = clip;
            target.Play();
            target.volume = volume;
        });
    }
}
