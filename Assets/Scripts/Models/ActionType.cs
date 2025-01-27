public enum ActionType
{
    //SIMPLES
    [ActionTypeName("You gain Bubbles!")]
    GAIN_BUBBLE,
    [ActionTypeName("You lose Bubbles...")]
    LOSE_BUBBLE,
    [ActionTypeName("Your Moxie goes up!")]
    GAIN_MOXIE,
    [ActionTypeName("Your Moxie goes down...")]
    LOSE_MOXIE,
    [ActionTypeName("Your Hijinks goes up!")]
    GAIN_HIJINKS,
    [ActionTypeName("Your Hijinks goes down...")]
    LOSE_HIJINKS,
    [ActionTypeName("No effect at all.")]
    DO_NOTHING,

    //GAIN BUBBLES
    [ActionTypeName("You gain some Bubbles but your Moxie goes down.")]
    GAIN_BUBBLE_LOSE_MOXIE,
    [ActionTypeName("You gain some Bubbles but your Hijinks goes down.")]
    GAIN_BUBBLE_LOSE_HIJINKS,
    [ActionTypeName("You gain some Bubbles and your Moxie goes up!")]
    GAIN_BUBBLE_GAIN_MOXIE,
    [ActionTypeName("You gain some Bubbles and your Hijinks goes up!")]
    GAIN_BUBBLE_GAIN_HIJINKS,

    //LOSE BUBBLES
    [ActionTypeName("Your Hijinks goes up but you lose some Bubbles.")]
    LOSE_BUBBLE_GAIN_HIJINKS,
    [ActionTypeName("Your Moxie goes up but you lose some Bubbles.")]
    LOSE_BUBBLE_GAIN_MOXIE,
    [ActionTypeName("You lose some Bubbles and your Moxie goes down...")]
    LOSE_BUBBLE_LOSE_MOXIE,
    [ActionTypeName("You lose some Bubbles and your Hijinks goes down...")]
    LOSE_BUBBLE_LOSE_HIJINKS,

    // NON-BUBBLE
    [ActionTypeName("Both your Moxie and you Hijinks go up!")]
    GAIN_MOXIE_GAIN_HIJINKS,
    [ActionTypeName("Both your Moxie and you Hijinks go down...")]
    LOSE_MOXIE_LOSE_HIJINKS,
    [ActionTypeName("Your Hijinks goes up and your Moxie goes down.")]
    GAIN_HIJINKS_LOSE_MOXIE,
    [ActionTypeName("Your Moxie goes up and your Hijinks goes down.")]
    GAIN_MOXIE_LOSE_HIJINKS
}