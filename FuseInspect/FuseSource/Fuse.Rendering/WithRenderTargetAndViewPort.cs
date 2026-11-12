using System;
using Stride.Graphics;
using Stride.Rendering;
using VL.Stride.Rendering;

namespace Fuse.Rendering;

public class WithRenderTargetAndViewPort : RendererBase
{
	private readonly ViewportState viewportState;

	public Texture RenderTarget { get; set; }

	public Texture DepthBuffer { get; set; }

	protected override void DrawInternal(RenderDrawContext context)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Texture renderTarget = RenderTarget;
		Texture depthBuffer = DepthBuffer;
		bool flag = renderTarget != null;
		bool flag2 = depthBuffer != null;
		if (flag | flag2)
		{
			RenderContext renderContext = context.RenderContext;
			RenderOutputRestore val = renderContext.SaveRenderOutputAndRestore();
			try
			{
				ViewportRestore val2 = renderContext.SaveViewportAndRestore();
				try
				{
					RenderTargetRestore val3 = context.PushRenderTargetsAndRestore();
					try
					{
						if (flag)
						{
							renderContext.RenderOutput.RenderTargetFormat0 = renderTarget.ViewFormat;
							renderContext.RenderOutput.RenderTargetCount = 1;
							renderContext.ViewportState = viewportState;
							renderContext.ViewportState.Viewport0 = new Viewport(0, 0, renderTarget.ViewWidth, renderTarget.ViewHeight);
						}
						context.CommandList.SetRenderTargetAndViewport(depthBuffer, renderTarget);
						((RendererBase)this).DrawInput(context);
						return;
					}
					finally
					{
						((IDisposable)val3/*cast due to constrained. prefix*/).Dispose();
					}
				}
				finally
				{
					((IDisposable)val2/*cast due to constrained. prefix*/).Dispose();
				}
			}
			finally
			{
				((IDisposable)val/*cast due to constrained. prefix*/).Dispose();
			}
		}
		((RendererBase)this).DrawInput(context);
	}
}
