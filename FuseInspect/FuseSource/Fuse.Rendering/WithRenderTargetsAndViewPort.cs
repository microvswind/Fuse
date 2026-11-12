using System;
using System.Collections.Generic;
using Stride.Graphics;
using Stride.Rendering;
using VL.Stride.Rendering;

namespace Fuse.Rendering;

public class WithRenderTargetsAndViewPort : RendererBase
{
	private readonly ViewportState viewportState;

	public List<Texture> RenderTargets { get; set; }

	public Texture DepthBuffer { get; set; }

	protected override void DrawInternal(RenderDrawContext context)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		Texture depthBuffer = DepthBuffer;
		bool flag = depthBuffer != null;
		Texture[] array = new Texture[RenderTargets.Count];
		bool flag2 = array.Length != 0;
		if (flag2 | flag)
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
						if (flag2)
						{
							for (int i = 0; i < RenderTargets.Count; i++)
							{
								array[i] = RenderTargets[i];
								switch (i)
								{
								case 0:
									renderContext.RenderOutput.RenderTargetFormat0 = array[i].ViewFormat;
									break;
								case 1:
									renderContext.RenderOutput.RenderTargetFormat1 = array[i].ViewFormat;
									break;
								case 2:
									renderContext.RenderOutput.RenderTargetFormat2 = array[i].ViewFormat;
									break;
								case 3:
									renderContext.RenderOutput.RenderTargetFormat3 = array[i].ViewFormat;
									break;
								case 4:
									renderContext.RenderOutput.RenderTargetFormat4 = array[i].ViewFormat;
									break;
								case 5:
									renderContext.RenderOutput.RenderTargetFormat5 = array[i].ViewFormat;
									break;
								case 6:
									renderContext.RenderOutput.RenderTargetFormat6 = array[i].ViewFormat;
									break;
								case 7:
									renderContext.RenderOutput.RenderTargetFormat7 = array[i].ViewFormat;
									break;
								}
							}
							renderContext.RenderOutput.RenderTargetCount = RenderTargets.Count;
							renderContext.ViewportState = viewportState;
							renderContext.ViewportState.Viewport0 = new Viewport(0, 0, array[0].ViewWidth, array[0].ViewHeight);
						}
						context.CommandList.SetRenderTargetsAndViewport(depthBuffer, array);
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
