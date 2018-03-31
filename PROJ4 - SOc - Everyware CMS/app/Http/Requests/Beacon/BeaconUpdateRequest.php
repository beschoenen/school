<?php

namespace App\Http\Requests\Beacon;

use Illuminate\Foundation\Http\FormRequest;

class BeaconUpdateRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            'name' => 'sometimes|required|string',
            'pointsOfInterest' => 'sometimes|required|array',
            'pointsOfInterest.*.name' => 'required|string',
            'pointsOfInterest.*.direction' => 'required|integer|min:1|max:36',
            'pointsOfInterest.*.description' => 'required|string',
            'pointsOfInterest.*.spokenDescription' => 'required|string',
        ];
    }
}
