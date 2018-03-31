<div id="{{ $image_modal_id }}" class="modal fade" role="dialog">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal">&times;</button>
				<h4 class="modal-title" id="modal-title">Selecteer een foto</h4>
			</div>
			<div class="modal-body">
				<div id="modal-image-container"></div>
			</div>
			<div class="modal-footer">
				<div id="modal-pagination-control" class="pull-left"></div>
				<button type="button" class="btn btn-default" data-dismiss="modal">Sluiten</button>
			</div>
		</div>
	</div>
</div>

<style>
	.modal-image-fragment {
		text-align: center;
	}
	.modal-image-fragment img {
		max-width: 100%;
		max-height: 100px;
		cursor: pointer;
		padding: 5px;
	}
	.modal-image-fragment img.selected {
		padding: 0;
		border: 5px solid blue;
		border-radius: 5px;
	}
	.modal-image-fragment span {
		display: inline-block;
		margin: 0 auto;
	}
	.modal-image-fragment-image-container {
		height: 100px;
	}
	.modal-footer .btn-primary {
		margin-right: 10px;
	}
</style>

@push('js')
<script>
	var modalId = '{{ $image_modal_id }}';
</script>
<script src="{{ asset('assets/js/image_modal.js') }}"></script>
@endpush