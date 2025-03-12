namespace Tools.Weapons.Firearms
{
    // ReSharper disable once InconsistentNaming
    public sealed class AK74 : FireArm
    {
        private void Update()
        {
            if (ShootAction.IsPressed())
            {
                Shoot();
            }
        }
    }
}
