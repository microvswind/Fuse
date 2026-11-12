using System;
using System.Collections.Generic;
using VL.Core.PublicAPI;

namespace Fuse.regions;

public abstract class FuseRegionBase<T> : IRegion<T>
{
	protected Func<T>? _patchInlayFactory;

	protected readonly Dictionary<InputDescription, object> _inputValues = new Dictionary<InputDescription, object>();

	protected readonly Dictionary<OutputDescription, object> _outputValues = new Dictionary<OutputDescription, object>();

	public virtual void AcknowledgeInput(in InputDescription description, object outerValue)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_inputValues[description] = outerValue;
	}

	public virtual void AcknowledgeOutput(in OutputDescription description, T patchInlay, object innerValue)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_outputValues[description] = innerValue;
	}

	public virtual void RetrieveInput(in InputDescription description, T patchInlay, out object innerValue)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_inputValues.TryGetValue(description, out innerValue);
	}

	public virtual void RetrieveOutput(in OutputDescription description, out object outerValue)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_outputValues.TryGetValue(description, out outerValue);
	}

	public virtual void SetPatchInlayFactory(Func<T> patchInlayFactory)
	{
		_patchInlayFactory = patchInlayFactory;
	}
}
