import { useForm } from 'react-hook-form';
import './CreatePost.css'; // Import the external CSS file

function CreatePost() {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm();

  const onSubmit = async (data) => {
    try {
      const response = await fetch('http://localhost:5220/api/posts', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('authToken')}`, // Include if authentication is required
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {
        reset(); // Clear the form after successful submission
        alert('Post created successfully!');
      } else {
        const errorMessage = await response.text();
        alert(`Failed to create post: ${errorMessage}`);
      }
    } catch (err) {
      alert('An error occurred while creating the post.');
      console.error(err);
    }
  };

  return (
    <div className="create-post-container">
      <h2>Create a New Post</h2>
      <form onSubmit={handleSubmit(onSubmit)} className="create-post-form">
        {/* Title Field */}
        <div className="form-field">
          <label>Title</label>
          <input
            type="text"
            placeholder="Enter the title of your post"
            {...register('title', {
              required: 'Title is required',
              maxLength: {
                value: 100,
                message: 'Title cannot exceed 100 characters',
              },
            })}
          />
          {errors.title && (
            <span className="error-message">{errors.title.message}</span>
          )}
        </div>

        {/* URL Field */}
        <div className="form-field">
          <label>URL</label>
          <input
            type="text"
            placeholder="Enter the URL"
            {...register('url', {
              required: 'URL is required',
              pattern: {
                value: /^(https?|chrome):\/\/[^\s$.?#].[^\s]*$/,
                message: 'Please enter a valid URL',
              },
            })}
          />
          {errors.url && (
            <span className="error-message">{errors.url.message}</span>
          )}
        </div>

        {/* Submit Button */}
        <button type="submit" disabled={isSubmitting} className="createbtn">
          {isSubmitting ? 'Submitting...' : 'Create Post'}
        </button>
      </form>
    </div>
  );
}

export default CreatePost;
