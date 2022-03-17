namespace RedPhase.Entities.Identity;

public class IdentityUser
{
    //
    // Summary:
    //     Gets or sets the primary key for this user.
    
    public virtual string Id
    {

        get;set;
    }

    //
    // Summary:
    //     Gets or sets the user name for this user.

    public virtual string UserName
    {

        get;

        set;
    }

    //
    // Summary:
    //     Gets or sets the normalized user name for this user.
    public virtual string NormalizedUserName
    {

        get;set;
    }

    //
    // Summary:
    //     Gets or sets the email address for this user.

    public virtual string Email
    {

        get;set;
    }

    //
    // Summary:
    //     Gets or sets the normalized email address for this user.
    public virtual string NormalizedEmail
    {

        get; set;
    }

    //
    // Summary:
    //     Gets or sets a flag indicating if a user has confirmed their email address.
    //
    // Value:
    //     True if the email address has been confirmed, otherwise false.
    
    public virtual bool EmailConfirmed
    {

        get; set;
    }     


    public virtual string PhoneNumber
    {

        get; set;
    }

    //
    // Summary:
    //     Gets or sets a flag indicating if a user has confirmed their telephone address.
    //
    // Value:
    //     True if the telephone number has been confirmed, otherwise false.
    
    public virtual bool PhoneNumberConfirmed
    {

        get; set;
    }

    //
    // Summary:
    //     Gets or sets a flag indicating if two factor authentication is enabled for this
    //     user.
    //
    // Value:
    //     True if 2fa is enabled, otherwise false.
    
    public virtual bool TwoFactorEnabled
    {

        get; set;
    }

    //
    // Summary:
    //     Gets or sets the date and time, in UTC, when any user lockout ends.
    //
    // Remarks:
    //     A value in the past means the user is not locked out.
    public virtual DateTimeOffset? LockoutEnd
    {

        get; set;
    }

    //
    // Summary:
    //     Gets or sets a flag indicating if the user could be locked out.
    //
    // Value:
    //     True if the user could be locked out, otherwise false.
    public virtual bool LockoutEnabled
    {

        get; set;
    }

    //
    // Summary:
    //     Gets or sets the number of failed login attempts for the current user.
    public virtual int AccessFailedCount
    {

        get; set;
    }
}
