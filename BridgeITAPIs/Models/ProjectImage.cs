using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class ProjectImage
{
    public Guid Id { get; set; }

    public Guid? ProjectId { get; set; }

    public string? ImageData { get; set; }

    public virtual Project? Project { get; set; }
}
