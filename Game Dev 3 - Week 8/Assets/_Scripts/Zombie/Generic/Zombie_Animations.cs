using UnityEngine;

namespace GameDevWithMarco.Enemy
{
    public class Zombie_Animations : MonoBehaviour
    {
        Animator anim;

        //Animation Clips
        public AnimationClip[] walkAnimations;
        public AnimationClip[] runAnimations;
        public AnimationClip[] idleAnimations;
        public AnimationClip[] attackAnimations;
        public AnimationClip[] screamAnimations;


        #region "Initialisation"

        private void Awake()
        {
            VariablesSetter();
        }
        // Start is called before the first frame update
        void Start()
        {

            RandomiseAnimationStart();
        }

        //This method will make sure that the zombies are not all moving in synchro
        private void RandomiseAnimationStart()
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        }

        private void VariablesSetter()
        {
            anim = GetComponent<Animator>();
        }

        #endregion


        #region "Animation Player Methods"
        public void PickRandomAnimationClip(AnimationClip[] clipsArray)
        {
            var randomClip = Random.Range(0, clipsArray.Length);    //Test a random number so we can randomise the idle animation

            string animationName = clipsArray[randomClip].name;     //Gets the name of the animation we have randomly picked

            anim.Play(animationName);                               //Playes the animation we have picked

            Debug.Log("The name of the currently played animation is: " + animationName);
        }

        public void PlayIdleAnimation()
        {
            PickRandomAnimationClip(idleAnimations);      //Uses the custom method we made to pick a random animation
        }

        public void PlayWalkAnimation()
        {
            PickRandomAnimationClip(walkAnimations);      //Uses the custom method we made to pick a random animation
        }


        public void PlayRunAnimation()
        {
            PickRandomAnimationClip(runAnimations);      //Uses the custom method we made to pick a random animation
        }

        public void PlayAttackAnimation()
        {
            PickRandomAnimationClip(attackAnimations);      //Uses the custom method we made to pick a random animation
        }

        public void PlayScreamAnimation()
        {
            PickRandomAnimationClip(screamAnimations);      //Uses the custom method we made to pick a random animation
        }

        #endregion


    }
}
